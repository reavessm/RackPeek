#!/usr/bin/env bash
set -euo pipefail

PROJECT_PATH="."
OUTPUT_FILE="COMMANDS.md"
declare -A VISITED

# ----------------------------
# Helpers
# ----------------------------

strip_colors() {
  sed -r "s/\x1B\[[0-9;]*[mK]//g"
}

run_help() {
  # $@ contains the parts, e.g., "switches" "summary" "list"
  echo "Running: dotnet run --project ./RackPeek -- \"$@\"" >&2

  local output
  if ! output=$(dotnet run --project ./RackPeek -- "$@" 2>&1 | strip_colors); then
    echo "WARNING: command failed: dotnet run --project ./RackPeek -- $*" >&2
    echo "$output" >&2
    return 1
  fi

  echo "$output"
}

# Extracts commands from help output
get_commands() {
  local help_output="$1"

  echo "$help_output" | awk '
    BEGIN { in_commands = 0 }

    /^COMMANDS:/ { in_commands = 1; next }

    in_commands {
      if ($0 ~ /^[[:space:]]{4}[a-zA-Z0-9-]+[[:space:]]+/) {
        print $1
        next
      }
      if ($0 !~ /^[[:space:]]*$/) {
        exit
      }
    }
  '
}

# ----------------------------
# Recursion
# ----------------------------

generate_help_recursive() {
  local current_path=("$@")
  
  # 1. Create a "Map Key" for the VISITED hash (must not be empty)
  local flat_cmd="${current_path[*]}"
  local map_key="${flat_cmd:-root}"

  # 2. Create a "Display Header" for the Markdown file
  #    If empty, just "rpk". If not empty, "rpk <commands>"
  local display_header
  if [[ -z "$flat_cmd" ]]; then
    display_header="rpk"
  else
    display_header="rpk $flat_cmd"
  fi

  # Prevent infinite loops using the Map Key
  if [[ -n "${VISITED["$map_key"]:-}" ]]; then
    return
  fi
  VISITED["$map_key"]=1

  # Run help
  local help_output
  if ! help_output=$(run_help "${current_path[@]}" --help); then
    echo "Skipping: $map_key (help failed)" >&2
    return
  fi

  # Append to Markdown file using the Display Header
  {
    echo "## \`${display_header}\`"
    echo '```'
    echo "$help_output"
    echo '```'
    echo ""
  } >> "$OUTPUT_FILE"

  # Extract subcommands
  local commands
  mapfile -t commands < <(get_commands "$help_output")

  for cmd in "${commands[@]}"; do
    echo "Recursing into: ${display_header} ${cmd}" >&2
    generate_help_recursive "${current_path[@]}" "$cmd"
  done
}

# ----------------------------
# Main
# ----------------------------

{
  echo "# CLI Commands"
  echo ""
} > "$OUTPUT_FILE"

generate_help_recursive

echo "Generated $OUTPUT_FILE successfully."