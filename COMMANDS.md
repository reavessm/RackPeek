# CLI Commands

## `rpk`
```
USAGE:
    rpk [OPTIONS] <COMMAND>

OPTIONS:
    -h, --help    Prints help information

COMMANDS:
    switches    Manage switches                  
    systems     Manage systems                   
    ap          Show access point hardware report
    desktops    Show desktop hardware report     
    ups         Show UPS hardware report         
    servers     Manage servers                   
```

## `rpk switches`
```
DESCRIPTION:
Manage switches

USAGE:
    rpk switches [OPTIONS] <COMMAND>

OPTIONS:
    -h, --help    Prints help information

COMMANDS:
    summary            Show switch hardware report             
    add <name>         Add a new switch                        
    list               List switches                           
    get <name>         Get a switches by name                  
    describe <name>    Show detailed information about a switch
    set <name>         Update switch properties                
    del <name>         Delete a switch                         
```

## `rpk switches summary`
```
DESCRIPTION:
Show switch hardware report

USAGE:
    rpk switches summary [OPTIONS]

OPTIONS:
    -h, --help    Prints help information
```

## `rpk switches add`
```
DESCRIPTION:
Add a new switch

USAGE:
    rpk switches add <name> [OPTIONS]

ARGUMENTS:
    <name>     

OPTIONS:
    -h, --help    Prints help information
```

## `rpk switches list`
```
DESCRIPTION:
List switches

USAGE:
    rpk switches list [OPTIONS]

OPTIONS:
    -h, --help    Prints help information
```

## `rpk switches get`
```
DESCRIPTION:
Get a switches by name

USAGE:
    rpk switches get <name> [OPTIONS]

ARGUMENTS:
    <name>     

OPTIONS:
    -h, --help    Prints help information
```

## `rpk switches describe`
```
DESCRIPTION:
Show detailed information about a switch

USAGE:
    rpk switches describe <name> [OPTIONS]

ARGUMENTS:
    <name>     

OPTIONS:
    -h, --help    Prints help information
```

## `rpk switches set`
```
DESCRIPTION:
Update switch properties

USAGE:
    rpk switches set <name> [OPTIONS]

ARGUMENTS:
    <name>     

OPTIONS:
    -h, --help       Prints help information
        --Model                             
        --managed                           
        --poe                               
```

## `rpk switches del`
```
DESCRIPTION:
Delete a switch

USAGE:
    rpk switches del <name> [OPTIONS]

ARGUMENTS:
    <name>     

OPTIONS:
    -h, --help    Prints help information
```

## `rpk systems`
```
DESCRIPTION:
Manage systems

USAGE:
    rpk systems [OPTIONS] <COMMAND>

OPTIONS:
    -h, --help    Prints help information

COMMANDS:
    summary            Show system report                      
    add <name>         Add a new system                        
    list               List systems                            
    get <name>         Get a system by name                    
    describe <name>    Show detailed information about a system
    set <name>         Update system properties                
    del <name>         Delete a system                         
```

## `rpk systems summary`
```
DESCRIPTION:
Show system report

USAGE:
    rpk systems summary [OPTIONS]

OPTIONS:
    -h, --help    Prints help information
```

## `rpk systems add`
```
DESCRIPTION:
Add a new system

USAGE:
    rpk systems add <name> [OPTIONS]

ARGUMENTS:
    <name>     

OPTIONS:
    -h, --help    Prints help information
```

## `rpk systems list`
```
DESCRIPTION:
List systems

USAGE:
    rpk systems list [OPTIONS]

OPTIONS:
    -h, --help    Prints help information
```

## `rpk systems get`
```
DESCRIPTION:
Get a system by name

USAGE:
    rpk systems get <name> [OPTIONS]

ARGUMENTS:
    <name>     

OPTIONS:
    -h, --help    Prints help information
```

## `rpk systems describe`
```
DESCRIPTION:
Show detailed information about a system

USAGE:
    rpk systems describe <name> [OPTIONS]

ARGUMENTS:
    <name>     

OPTIONS:
    -h, --help    Prints help information
```

## `rpk systems set`
```
DESCRIPTION:
Update system properties

USAGE:
    rpk systems set <name> [OPTIONS]

ARGUMENTS:
    <name>     

OPTIONS:
    -h, --help       Prints help information
        --type                              
        --os                                
        --cores                             
        --ram                               
        --runs-on                           
```

## `rpk systems del`
```
DESCRIPTION:
Delete a system

USAGE:
    rpk systems del <name> [OPTIONS]

ARGUMENTS:
    <name>     

OPTIONS:
    -h, --help    Prints help information
```

## `rpk ap`
```
DESCRIPTION:
Show access point hardware report

USAGE:
    rpk ap [OPTIONS]

OPTIONS:
    -h, --help    Prints help information
```

## `rpk desktops`
```
DESCRIPTION:
Show desktop hardware report

USAGE:
    rpk desktops [OPTIONS]

OPTIONS:
    -h, --help    Prints help information
```

## `rpk ups`
```
DESCRIPTION:
Show UPS hardware report

USAGE:
    rpk ups [OPTIONS]

OPTIONS:
    -h, --help    Prints help information
```

## `rpk servers`
```
DESCRIPTION:
Manage servers

USAGE:
    rpk servers [OPTIONS] <COMMAND>

OPTIONS:
    -h, --help    Prints help information

COMMANDS:
    summary            Show server hardware report              
    add <name>         Add a new server                         
    get <name>         List servers or get a server by name     
    describe <name>    Show detailed information about a server 
    set <name>         Update server properties                 
    del <name>         Delete a server                          
    tree <name>        Displays a dependency tree for the server
    cpu                Manage server CPUs                       
    drive              Manage server drives                     
    gpu                Manage server GPUs                       
    nic                Manage server NICs                       
```

## `rpk servers summary`
```
DESCRIPTION:
Show server hardware report

USAGE:
    rpk servers summary [OPTIONS]

OPTIONS:
    -h, --help    Prints help information
```

## `rpk servers add`
```
DESCRIPTION:
Add a new server

USAGE:
    rpk servers add <name> [OPTIONS]

ARGUMENTS:
    <name>     

OPTIONS:
    -h, --help    Prints help information
```

## `rpk servers get`
```
DESCRIPTION:
List servers or get a server by name

USAGE:
    rpk servers get <name> [OPTIONS]

ARGUMENTS:
    <name>     

OPTIONS:
    -h, --help    Prints help information
```

## `rpk servers describe`
```
DESCRIPTION:
Show detailed information about a server

USAGE:
    rpk servers describe <name> [OPTIONS]

ARGUMENTS:
    <name>     

OPTIONS:
    -h, --help    Prints help information
```

## `rpk servers set`
```
DESCRIPTION:
Update server properties

USAGE:
    rpk servers set <name> [OPTIONS]

ARGUMENTS:
    <name>     

OPTIONS:
    -h, --help        Prints help information
        --ram <GB>                           
        --ipmi                               
```

## `rpk servers del`
```
DESCRIPTION:
Delete a server

USAGE:
    rpk servers del <name> [OPTIONS]

ARGUMENTS:
    <name>     

OPTIONS:
    -h, --help    Prints help information
```

## `rpk servers tree`
```
DESCRIPTION:
Displays a dependency tree for the server

USAGE:
    rpk servers tree <name> [OPTIONS]

ARGUMENTS:
    <name>     

OPTIONS:
    -h, --help    Prints help information
```

## `rpk servers cpu`
```
DESCRIPTION:
Manage server CPUs

USAGE:
    rpk servers cpu [OPTIONS] <COMMAND>

OPTIONS:
    -h, --help    Prints help information

COMMANDS:
    add <name>    Add a CPU to a server     
    set <name>    Update a CPU on a server  
    del <name>    Remove a CPU from a server
```

## `rpk servers cpu add`
```
DESCRIPTION:
Add a CPU to a server

USAGE:
    rpk servers cpu add <name> [OPTIONS]

ARGUMENTS:
    <name>     

OPTIONS:
    -h, --help                 Prints help information
        --model <MODEL>                               
        --cores <CORES>                               
        --threads <THREADS>                           
```

## `rpk servers cpu set`
```
DESCRIPTION:
Update a CPU on a server

USAGE:
    rpk servers cpu set <name> [OPTIONS]

ARGUMENTS:
    <name>     

OPTIONS:
    -h, --help                 Prints help information
        --index <INDEX>                               
        --model <MODEL>                               
        --cores <CORES>                               
        --threads <THREADS>                           
```

## `rpk servers cpu del`
```
DESCRIPTION:
Remove a CPU from a server

USAGE:
    rpk servers cpu del <name> [OPTIONS]

ARGUMENTS:
    <name>     

OPTIONS:
    -h, --help             Prints help information
        --index <INDEX>                           
```

## `rpk servers drive`
```
DESCRIPTION:
Manage server drives

USAGE:
    rpk servers drive [OPTIONS] <COMMAND>

OPTIONS:
    -h, --help    Prints help information

COMMANDS:
    add <name>    Add a drive to a server     
    set <name>    Update a drive on a server  
    del <name>    Remove a drive from a server
```

## `rpk servers drive add`
```
DESCRIPTION:
Add a drive to a server

USAGE:
    rpk servers drive add <name> [OPTIONS]

ARGUMENTS:
    <name>     

OPTIONS:
    -h, --help           Prints help information
        --type <TYPE>                           
        --size <SIZE>                           
```

## `rpk servers drive set`
```
DESCRIPTION:
Update a drive on a server

USAGE:
    rpk servers drive set <name> [OPTIONS]

ARGUMENTS:
    <name>     

OPTIONS:
    -h, --help             Prints help information
        --index <INDEX>                           
        --type <TYPE>                             
        --size <SIZE>                             
```

## `rpk servers drive del`
```
DESCRIPTION:
Remove a drive from a server

USAGE:
    rpk servers drive del <name> [OPTIONS]

ARGUMENTS:
    <name>     

OPTIONS:
    -h, --help             Prints help information
        --index <INDEX>                           
```

## `rpk servers gpu`
```
DESCRIPTION:
Manage server GPUs

USAGE:
    rpk servers gpu [OPTIONS] <COMMAND>

OPTIONS:
    -h, --help    Prints help information

COMMANDS:
    add <name>    Add a GPU to a server     
    set <name>    Update a GPU on a server  
    del <name>    Remove a GPU from a server
```

## `rpk servers gpu add`
```
DESCRIPTION:
Add a GPU to a server

USAGE:
    rpk servers gpu add <name> [OPTIONS]

ARGUMENTS:
    <name>     

OPTIONS:
    -h, --help             Prints help information
        --model <MODEL>                           
        --vram <VRAM>                             
```

## `rpk servers gpu set`
```
DESCRIPTION:
Update a GPU on a server

USAGE:
    rpk servers gpu set <name> [OPTIONS]

ARGUMENTS:
    <name>     

OPTIONS:
    -h, --help             Prints help information
        --index <INDEX>                           
        --model <MODEL>                           
        --vram <VRAM>                             
```

## `rpk servers gpu del`
```
DESCRIPTION:
Remove a GPU from a server

USAGE:
    rpk servers gpu del <name> [OPTIONS]

ARGUMENTS:
    <name>     

OPTIONS:
    -h, --help             Prints help information
        --index <INDEX>                           
```

## `rpk servers nic`
```
DESCRIPTION:
Manage server NICs

USAGE:
    rpk servers nic [OPTIONS] <COMMAND>

OPTIONS:
    -h, --help    Prints help information

COMMANDS:
    add <name>    Add a NIC to a server     
    set <name>    Update a NIC on a server  
    del <name>    Remove a NIC from a server
```

## `rpk servers nic add`
```
DESCRIPTION:
Add a NIC to a server

USAGE:
    rpk servers nic add <name> [OPTIONS]

ARGUMENTS:
    <name>     

OPTIONS:
    -h, --help             Prints help information
        --type <TYPE>                             
        --speed <SPEED>                           
        --ports <PORTS>                           
```

## `rpk servers nic set`
```
DESCRIPTION:
Update a NIC on a server

USAGE:
    rpk servers nic set <name> [OPTIONS]

ARGUMENTS:
    <name>     

OPTIONS:
    -h, --help             Prints help information
        --index <INDEX>                           
        --type <TYPE>                             
        --speed <SPEED>                           
        --ports <PORTS>                           
```

## `rpk servers nic del`
```
DESCRIPTION:
Remove a NIC from a server

USAGE:
    rpk servers nic del <name> [OPTIONS]

ARGUMENTS:
    <name>     

OPTIONS:
    -h, --help             Prints help information
        --index <INDEX>                           
```

