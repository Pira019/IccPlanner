/*
Ce fichier permet de init le profile Admin dans la base de donnee
*/

INSERT INTO "Profiles" ("Name" , "Description" , "CreatedAt" , "UpdatedAt") 
VALUES ('ADMIN', 'ADMIN', NOW(), NOW());
