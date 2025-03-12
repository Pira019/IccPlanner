/*
Ce fichier permet de init les les donnees dans la base de donnee
*/

INSERT INTO "Postes" ("Name", "Description")
VALUES 
    ('Responsable/Référent', 'Description'),
    ('Planificateur', 'Description');

INSERT INTO "Roles" ("Id","Name", "Description","Discriminator","NormalizedName")
VALUES 
    (1,'Admin', 'Description','Role','ADMIN');




