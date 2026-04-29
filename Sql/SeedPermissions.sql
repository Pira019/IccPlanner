/*
    Seed des permissions dans la table Permissions.
    Utilise WHERE NOT EXISTS pour ne pas créer de doublons.
*/

-- Rôles
INSERT INTO "Permissions" ("Name", "Description", "Fnc")
SELECT 'CanReadRole', 'Peut lire un rôle', 'Role'
WHERE NOT EXISTS (SELECT 1 FROM "Permissions" WHERE "Name" = 'CanReadRole');

INSERT INTO "Permissions" ("Name", "Description", "Fnc")
SELECT 'CanCreateRole', 'Peut créer un rôle', 'Role'
WHERE NOT EXISTS (SELECT 1 FROM "Permissions" WHERE "Name" = 'CanCreateRole');

-- Ministère
INSERT INTO "Permissions" ("Name", "Description", "Fnc")
SELECT 'CanCreateMinistry', 'Peut gérer un ministère', 'Ministry'
WHERE NOT EXISTS (SELECT 1 FROM "Permissions" WHERE "Name" = 'CanCreateMinistry');

-- Département
INSERT INTO "Permissions" ("Name", "Description", "Fnc")
SELECT 'CanManagDepart', 'Peut gérer un département (Créer, Supprimer et Modifier)', 'Dept'
WHERE NOT EXISTS (SELECT 1 FROM "Permissions" WHERE "Name" = 'CanManagDepart');

INSERT INTO "Permissions" ("Name", "Description", "Fnc")
SELECT 'depart:manager', 'Droit de gestion sur le département auquel l''utilisateur est rattaché', 'Dept'
WHERE NOT EXISTS (SELECT 1 FROM "Permissions" WHERE "Name" = 'depart:manager');

INSERT INTO "Permissions" ("Name", "Description", "Fnc")
SELECT 'manage_program_details', 'Permet de gérer les détails des programmes d''un département (dates, type)', 'Dept'
WHERE NOT EXISTS (SELECT 1 FROM "Permissions" WHERE "Name" = 'manage_program_details');

-- Programme
INSERT INTO "Permissions" ("Name", "Description", "Fnc")
SELECT 'prg:manager', 'Peut gérer un programme (Créer, Supprimer et Modifier)', 'Prg'
WHERE NOT EXISTS (SELECT 1 FROM "Permissions" WHERE "Name" = 'prg:manager');

-- Service
INSERT INTO "Permissions" ("Name", "Description", "Fnc")
SELECT 'ManagerService', 'Peut gérer les services', 'Service'
WHERE NOT EXISTS (SELECT 1 FROM "Permissions" WHERE "Name" = 'ManagerService');
