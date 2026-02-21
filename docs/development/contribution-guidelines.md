# Contribution Guidelines

- Keep changes small.
- Keep behaviour tested.
- Discuss before building.

## Checklist

* [ ] Linked GitHub issue
* [ ] Approach validated
* [ ] Small, focused PR
* [ ] All tests passing locally
* [ ] Behaviour covered by tests
* [ ] YAML migration defined (if needed)
  
## Before You Start

* Search existing **GitHub Issues**
* If none exists → create one (and seek feedback) before coding
* Clearly state what you want to work on
* Validate your approach with core maintainers
* Prefer GitHub Issues over Discord for design discussion

## Keep PRs Small

* Small, focused PRs only
* One concern per PR

Large PRs slow review and increase load on the team

## Open as Draft

* Mark PR as **Draft** initially
* Move to Ready only when:

  * Tests pass locally
  * Scope is complete
  * No debug code remains


## Testing Requirements

Before removing Draft:

* CLI tests pass
* Web UI E2E tests pass
* Behaviour is covered by tests

## YAML Schema Changes

If you modify persisted YAML:

* Define a schema migration
* Ensure backward compatibility (or clearly document breaking changes)
* Add migration tests

