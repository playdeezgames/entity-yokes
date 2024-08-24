# Entity Yokes

- [ ] 20240824 Entity Yokes Episode 4 - 
- [x] 20240821 Entity Yokes Episode 3 - 
- [x] 20240819 Entity Yokes Episode 2 - 
- [x] 20240818 Entity Yokes Episode 1 - 

# Requirements

- [x] I can create Entities
    - [x] Entities have an Identifier
        - [x] Identifier uniquely identify an entity
    - [x] Entities have an EntityType
- [x] I can retrieve all Entities
- [x] I can retrieve one entity by identifier
- [x] I can retrieve entities by entity type
- [x] I can destroy and entity
- [x] I can check a flag on an entity
- [x] I can set a flag on an entity
- [x] I am prevented from destroying an entity with flags
- [x] I can retrieve all flags for an entity
- [x] I can clear a flag on an entity
- [x] I can set a metadata on an entity with a string key and string value
- [x] I can retrieve a metadata from an entity by key
- [x] I can update metadata of an entity by key
- [x] I can list metadata keys for an entity
- [x] I can clear a metadata by key for an entity
- [x] I can set a counter on an entity with a string key and integer value
- [x] I can retrieve a counter from an entity by key
- [x] I can update counter of an entity by key
- [x] I can list counter keys for an entity
- [x] I can clear a counter for an entity by key
- [x] I can set a statistic on an entity with a string key and a double value
- [x] I can retrieve a statistic from an entity by key
- [x] I can update statistic by key for statistic
- [x] I can list statistic keys for an entity
- [x] I can clear a statistic for an entity by key 
- [x] I can create yokes of a yoke type from an entity to another entity
    - [x] The from and to entities may be the same
- [x] I can list yoked-to entities of a type for an entity
- [x] I can list yoked-from entities of a type for an entity
- [x] Yokes make an entity non-empty and thus not destroyable
- [x] Yokes can be destroyed
- [x] Yokes have to be empty to be destroyed
- [x] Yokes have flags
    - [x] Set flag
    - [x] Check for flag
    - [x] Read all flags
    - [x] Clear flag
- [x] Yokes have metadatas
    - [x] Set metadata
    - [x] Read metadata
    - [x] Update metadata
    - [x] List metadata keys
    - [x] Clear metadata
- [x] Yokes have counters
    - [x] Set counter
    - [x] Read counter
    - [x] Update counter
    - [x] List counter keys
    - [x] Clear counter
- [x] Yokes have statistics
    - [x] Set statistic
    - [x] Read statistic
    - [x] Update statistic
    - [x] List statistic keys
    - [x] Clear statistic
- [ ] Create SQLite backing store
- [ ] Minesweeper Example
    - [ ] EntityType: Cell
        - [ ] Counters: Column(0..Columns-1), Row(0..Rows-1)
        - [ ] Flags: Revealed, Bomb
        - [ ] Yokes: 
            - [ ] YokeType: Neighbor
                - [ ] Counters: Direction (0..3)

