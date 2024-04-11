Create table adres(
    id int (10) unsigned NOT NULL AUTO_INCREMENT,
    PIndex int(10) unsigned not null,
    city varchar(255) not null,
    street varchar(255) not null,
    Nhouse int(10) unsigned not null,
    NApartament int(10) unsigned,
    NStructure int(10) unsigned,
    PRIMARY KEY(id),
    FOREIGN KEY (id) REFERENCES org(id) ON DELETE CASCADE ON UPDATE CASCADE
)