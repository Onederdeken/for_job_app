CREATE TABLE Employees(
    id int (10) unsigned NOT NULL AUTO_INCREMENT,
    NameE varchar(254) NOT NULL,
    Secondname varchar(254) NOT NULL,
    otche varchar(254) NOT NULL,
    Dates date not null,
    Pser varchar(4) not null,
    Pnumb varchar(6) not null,
    CompanyId int (10) unsigned,
    PRIMARY KEY(id),
    FOREIGN KEY (CompanyId) REFERENCES org(id) ON DELETE CASCADE ON UPDATE CASCADE
)