# Numéro d'identification

| Nom du cas | Type |
|---|---|
| elgoPlan.Plan.01 | Fonction |
| Gérer les planning | |

**Domaine** : planification elgo (elgoplan) – GÉRER LES PLANNING

## Version

| Date | Intervenant | Description |
|---|---|---|
| 04 avril 2026 | Pires Lufungula | Première ébauche |
| 08 avril 2026 | Pires Lufungula | Ajout Retirer un membre, mise à jour des extensions |

## Acteurs impliqués

## Contexte d'utilisation

Ce cas d'utilisation décrit comment un gestionnaire peut assigner les membres disponibles au planning mensuel d'un département.

## Description

**Scénario principal :** Assigner un membre
- Extensions
- Extensions d'erreurs, d'avertissements et d'informations

**Scénario alternatif :** Retirer un membre
- Extensions
- Extensions d'erreurs, d'avertissements et d'informations

**Scénario alternatif :** Publier le planning
- Extensions
- Extensions d'erreurs, d'avertissements et d'informations

## Particularité

Aucune

## Précondition

- Aucune

## Cas d'utilisation appelant

Aucun

## Cas d'utilisation appelé

Voir AV.02 Interroger les disponibilités

---

## Scénario principal : Assigner un membre

> Url : /api/plannings/{departmentId}
> Méthode : POST

1. Le gestionnaire accède à la page Planning et sélectionne un département

2. Le système vérifie les droits

   > Voir AV.02 Interroger les disponibilités

3. Le gestionnaire choisit un poste pour le membre et clique « Affecter »

4. Le système vérifie qu'un PlanningPeriod existe pour ce département/mois/année, sinon le crée

   > Le système recherche un enregistrement dans la table PlanningPeriods (PP)
   > Pour lequel :
   > - PP.DepartmentId = « departmentId »
   > - ET PP.Month = « mois de la date sélectionnée »
   > - ET PP.Year = « année de la date sélectionnée »

   > S'il n'existe aucun enregistrement, le système crée un nouveau PlanningPeriod avec :
   > - PP.DepartmentId = « departmentId »
   > - PP.Month = « mois de la date sélectionnée »
   > - PP.Year = « année de la date sélectionnée »
   > - PP.IndPublished = « False »
   > - PP.IndArchived = « False »
   > - PP.CreatedAt = « date et heure courante »

5. Le système crée un enregistrement dans la table Planning lié à l'Availability et au PlanningPeriod

6. Le membre apparaît comme « planifié » dans la liste

### Extensions

**3a. L'utilisateur n'a pas les droits sur ce département**

> L'utilisateur n'a pas le droit d'assigner s'il n'existe aucun enregistrement dans la table DepartmentMembers (DM)
> Pour lequel :
> - DM.DepartmentId = « departmentId »
> - ET DM.MemberId = « id du membre connecté »
> - ET DM.IndPlanning = « True »

3a.1 Le système affiche le message d'erreur et refuse l'assignation

> Erreur : PLANNING_NOT_AUTHORIZED
> Message suggéré :
> - Fr : Vous n'êtes pas autorisé à gérer le planning de ce département.
> - En : You are not authorized to manage the planning for this department.


**3b. La disponibilité n'existe pas ou a été supprimée**

> La disponibilité n'existe pas s'il n'existe aucun enregistrement dans la table Availabilities (A)
> Pour lequel :
> - A.Id = « availabilityId »
> - ET A.IsDeleted = « False »

3b.1 Le système affiche le message d'erreur et refuse l'assignation

> Erreur : PLANNING_AVAILABILITY_NOT_FOUND
> Message suggéré :
> - Fr : La disponibilité est introuvable ou a été retirée par le membre.
> - En : The availability was not found or has been removed by the member.

**3c. Le planning du mois est archivé**

> Le planning est archivé s'il existe un enregistrement dans la table PlanningPeriods (PP)
> Pour lequel :
> - PP.DepartmentId = « departmentId »
> - ET PP.Month = « mois de la date sélectionnée »
> - ET PP.Year = « année de la date sélectionnée »
> - ET PP.IndArchived = « True »

3c.1 Le système affiche le message d'erreur et refuse l'assignation

> Erreur : PLANNING_ARCHIVED
> Message suggéré :
> - Fr : Le planning de ce mois est archivé et ne peut plus être modifié.
> - En : The planning for this month is archived and can no longer be modified.

**3d. Le membre est déjà assigné à ce service**

> Le membre est déjà assigné s'il existe un enregistrement dans la table Plannings (PL)
> Pour lequel :
> - PL.AvailabilityId = « availabilityId »

3d.1 Le système affiche le message d'erreur et refuse l'assignation

> Erreur : PLANNING_MEMBER_ALREADY_ASSIGNED
> Message suggéré :
> - Fr : Ce membre est déjà assigné à ce service.
> - En : This member is already assigned to this service.

**3e. La date du programme est passée**

> La date est passée si la date du programme associée à la disponibilité est antérieure à la date du jour.
>
> La date du programme est récupérée depuis la table Availabilities (A)
> - Jointe à la table TabServicePrgs (TSP) sur A.TabServicePrgId = TSP.Id
> - TSP jointe à la table PrgDates (PD) sur TSP.PrgDateId = PD.Id
>
> Pour lequel :
> - PD.Date < « date du jour »

3e.1 Le système affiche le message d'erreur et refuse l'assignation

> Erreur : PLANNING_PAST_DATE
> Message suggéré :
> - Fr : Vous ne pouvez pas modifier le planning d'une date passée.
> - En : You cannot modify a planning for a past date.

**3g. Chevauchement horaire (tous départements)**

> Le système vérifie s'il existe un conflit horaire pour le membre à assigner.
>
> Le membre a un conflit horaire s'il existe un enregistrement dans la table Plannings (PL)
> - Jointe à la table PlanningPeriods (PP) sur PL.PlanningPeriodId = PP.Id
> - Jointe à la table Availabilities (A2) sur PL.AvailabilityId = A2.Id
> - A2 jointe à la table DepartmentMembers (DM2) sur A2.DepartmentMemberId = DM2.Id
> - A2 jointe à la table TabServicePrgs (TSP2) sur A2.TabServicePrgId = TSP2.Id
> - TSP2 jointe à la table TabServices (TS2) sur TSP2.TabServicesId = TS2.Id
> - TSP2 jointe à la table PrgDates (PD2) sur TSP2.PrgDateId = PD2.Id
>
> Pour lequel :
> - DM2.MemberId = « MemberId du membre à assigner »
> - ET PD2.Date = « même date que la disponibilité à assigner »
> - ET TS2.StartTime < « EndTime du service à assigner »
> - ET TS2.EndTime > « StartTime du service à assigner »
> - ET PP.IndArchived = « False »

3g.1 Le système affiche un avertissement avec les détails du conflit dans un popup de confirmation

> Avertissement : PLANNING_OVERLAP
> Message suggéré :
> - Fr : Cette personne est déjà programmée sur « {serviceName} » ({startTime}-{endTime}) - {departmentName}.
> - En : This person is already scheduled on "{serviceName}" ({startTime}-{endTime}) - {departmentName}.

3g.2 Si le gestionnaire clique « Forcer l'assignation », le système relance l'assignation avec ForceAssign = True et ignore le chevauchement.


---

## Scénario alternatif : Retirer un membre

> Url : /api/plannings/{planningId}
> Méthode : DELETE

1. Le gestionnaire clique sur « Retirer » à côté d'un membre assigné dans la sidebar

2. Le système vérifie que le planning existe

   > Le système recherche un enregistrement dans la table Plannings (PL)
   > - Jointe à la table PlanningPeriods (PP) sur PL.PlanningPeriodId = PP.Id
   >
   > Pour lequel :
   > - PL.Id = « planningId »

3. Le système vérifie les droits de l'utilisateur

   > Le système recherche un enregistrement dans la table DepartmentMembers (DM)
   > Pour lequel :
   > - DM.DepartmentId = PP.DepartmentId
   > - ET DM.MemberId = « id du membre connecté »
   > - ET DM.IndPlanning = « True »

4. Le système vérifie que le planning n'est pas archivé

   > PP.IndArchived = « False »

5. Le système vérifie que la date n'est pas passée

   > PL.ProgramDate >= « date du jour »

6. Le système supprime l'enregistrement Planning

7. Le système retourne 204 No Content

8. Le membre repasse en état « non assigné » dans la sidebar et le calendrier se rafraîchit

### Extensions

**2a. Le planning n'existe pas**

> Le planning n'existe pas s'il n'existe aucun enregistrement dans la table Plannings (PL)
> Pour lequel :
> - PL.Id = « planningId »

2a.1 Le système affiche le message d'erreur et refuse le retrait

> Erreur : PLANNING_NOT_FOUND
> Message suggéré :
> - Fr : L'assignation au planning est introuvable.
> - En : The planning assignment was not found.

**3a. L'utilisateur n'a pas les droits sur ce département**

> L'utilisateur n'a pas le droit de retirer s'il n'existe aucun enregistrement dans la table DepartmentMembers (DM)
> Pour lequel :
> - DM.DepartmentId = PP.DepartmentId
> - ET DM.MemberId = « id du membre connecté »
> - ET DM.IndPlanning = « True »

3a.1 Le système affiche le message d'erreur et refuse le retrait

> Erreur : PLANNING_NOT_AUTHORIZED
> Message suggéré :
> - Fr : Vous n'êtes pas autorisé à gérer le planning de ce département.
> - En : You are not authorized to manage the planning for this department.

**4a. Le planning du mois est archivé**

> Le planning est archivé si PP.IndArchived = « True »

4a.1 Le système affiche le message d'erreur et refuse le retrait

> Erreur : PLANNING_ARCHIVED
> Message suggéré :
> - Fr : Le planning de ce mois est archivé et ne peut plus être modifié.
> - En : The planning for this month is archived and can no longer be modified.

**5a. La date du programme est passée**

> La date est passée si PL.ProgramDate < « date du jour »

5a.1 Le système affiche le message d'erreur et refuse le retrait

> Erreur : PLANNING_PAST_DATE
> Message suggéré :
> - Fr : Vous ne pouvez pas modifier le planning d'une date passée.
> - En : You cannot modify a planning for a past date.

---

## Références techniques

### Modèle de requête — Assigner un membre

| Propriété | Type | Description |
|---|---|---|
| AvailabilityId | int | Identifiant de la disponibilité à assigner |
| PosteId | int? | Identifiant du poste (nullable) |
| Comment | string? | Commentaire optionnel (max 500 caractères) |
| IndTraining | bool | Indique si c'est une formation. Par défaut False |
| IndObservation | bool | Indique si c'est une observation. Par défaut False |
| ForceAssign | bool | Si True, ignore le chevauchement horaire. Par défaut False |

### Modèle de requête — Retirer un membre

| Propriété | Type | Source | Description |
|---|---|---|---|
| planningId | int | Route | Identifiant du planning à supprimer |


---

## Scénario alternatif : Modifier une assignation

> Url : /api/plannings/{planningId}
> Méthode : PUT

1. Le gestionnaire modifie le poste ou la formation d'un membre déjà assigné et clique « Modifier »

2. Le système vérifie que le planning existe

   > Le système recherche un enregistrement dans la table Plannings (PL)
   > - Jointe à la table PlanningPeriods (PP) sur PL.PlanningPeriodId = PP.Id
   >
   > Pour lequel :
   > - PL.Id = « planningId »

3. Le système vérifie les droits de l'utilisateur

   > Le système recherche un enregistrement dans la table DepartmentMembers (DM)
   > Pour lequel :
   > - DM.DepartmentId = PP.DepartmentId
   > - ET DM.MemberId = « id du membre connecté »
   > - ET DM.IndPlanning = « True »

4. Le système vérifie que le planning n'est pas archivé

   > PP.IndArchived = « False »

5. Le système vérifie que la date n'est pas passée

   > PL.ProgramDate >= « date du jour »

6. Le système met à jour les champs suivants :

   > PL.PosteId = « posteId »
   > PL.IndTraining = « indTraining »
   > PL.IndObservation = « indObservation »
   > PL.Comment = « comment »
   > PL.UpdatedById = « id du membre connecté »

7. Le système retourne 204 No Content

8. Le calendrier se rafraîchit avec les nouvelles informations

### Extensions

**2a. Le planning n'existe pas**

2a.1 Le système affiche le message d'erreur et refuse la modification

> Erreur : PLANNING_NOT_FOUND
> Message suggéré :
> - Fr : L'assignation au planning est introuvable.
> - En : The planning assignment was not found.

**3a. L'utilisateur n'a pas les droits sur ce département**

3a.1 Le système affiche le message d'erreur et refuse la modification

> Erreur : PLANNING_NOT_AUTHORIZED
> Message suggéré :
> - Fr : Vous n'êtes pas autorisé à gérer le planning de ce département.
> - En : You are not authorized to manage the planning for this department.

**4a. Le planning du mois est archivé**

4a.1 Le système affiche le message d'erreur et refuse la modification

> Erreur : PLANNING_ARCHIVED
> Message suggéré :
> - Fr : Le planning de ce mois est archivé et ne peut plus être modifié.
> - En : The planning for this month is archived and can no longer be modified.

**5a. La date du programme est passée**

5a.1 Le système affiche le message d'erreur et refuse la modification

> Erreur : PLANNING_PAST_DATE
> Message suggéré :
> - Fr : Vous ne pouvez pas modifier le planning d'une date passée.
> - En : You cannot modify a planning for a past date.

### Modèle de requête — Modifier une assignation

| Propriété | Type | Description |
|---|---|---|
| PosteId | int? | Identifiant du poste (nullable) |
| Comment | string? | Commentaire optionnel (max 500 caractères) |
| IndTraining | bool | Indique si c'est une formation. Par défaut False |
| IndObservation | bool | Indique si c'est une observation. Par défaut False |


---

## Scénario alternatif : Publier le planning

> Url : /api/plannings/{departmentId}/publish?month={month}&year={year}
> Méthode : POST

1. Le gestionnaire clique sur « Publier le planning »

2. Le système vérifie les droits de l'utilisateur

   > Le système recherche un enregistrement dans la table DepartmentMembers (DM)
   > Pour lequel :
   > - DM.DepartmentId = « departmentId »
   > - ET DM.MemberId = « id du membre connecté »
   > - ET DM.IndPlanning = « True »

3. Le système vérifie qu'un PlanningPeriod existe

   > Le système recherche un enregistrement dans la table PlanningPeriods (PP)
   > Pour lequel :
   > - PP.DepartmentId = « departmentId »
   > - ET PP.Month = « month »
   > - ET PP.Year = « year »

4. Le système vérifie que le planning n'est pas archivé

   > PP.IndArchived = « False »

5. Le système supprime l'ancien snapshot publié

   > Le système supprime tous les enregistrements dans la table PublishedPlannings (PUB)
   > Pour lesquels :
   > - PUB.PlanningPeriodId = PP.Id

6. Le système crée le nouveau snapshot

   > Le système copie tous les enregistrements de la table Plannings (PL)
   > Pour lesquels :
   > - PL.PlanningPeriodId = PP.Id
   >
   > Dans la table PublishedPlannings avec les données dénormalisées :
   > - PUB.SourcePlanningId = PL.Id
   > - PUB.MemberName = PL.MemberName
   > - PUB.MemberId = (depuis Availability → DepartmentMember)
   > - PUB.ProgramDate = PL.ProgramDate
   > - PUB.ProgramName = (depuis Availability → TabServicePrg → PrgDate → DepartmentProgram → Program)
   > - PUB.ServiceName = (depuis Availability → TabServicePrg.DisplayName)
   > - PUB.PosteName = (depuis Poste.Name)
   > - PUB.IndTraining = PL.IndTraining
   > - PUB.PublishedAt = « date et heure courante »

7. Le système met à jour le PlanningPeriod

   > PP.IndPublished = « True »
   > PP.PublishedAt = « date et heure courante »
   > PP.PublishedById = « id du membre connecté »

8. Le système retourne 200 OK

### Comportement de publication

- Chaque assignation, retrait ou modification repasse PP.IndPublished à « False »
- Les membres consultent uniquement la table PublishedPlannings (snapshot figé)
- Le gestionnaire voit la table Plannings (brouillon en cours)
- Le bouton « Publier » affiche « Publié ✓ » si IndPublished = True, sinon « Publier »
- Un badge « Modifications non publiées » apparaît si IndPublished = False et PublishedAt n'est pas null (déjà publié au moins une fois)

### Extensions

**2a. L'utilisateur n'a pas les droits**

2a.1 Le système affiche le message d'erreur et refuse la publication

> Erreur : PLANNING_NOT_AUTHORIZED

**3a. Le PlanningPeriod n'existe pas**

3a.1 Le système affiche le message d'erreur

> Erreur : PLANNING_NOT_FOUND

**4a. Le planning est archivé**

4a.1 Le système affiche le message d'erreur

> Erreur : PLANNING_ARCHIVED

### Endpoint de statut

> Url : /api/plannings/{month}/{year}/status?departmentId={id}
> Méthode : GET

Retourne le statut du PlanningPeriod :

```json
{
  "indPublished": true,
  "indArchived": false,
  "publishedAt": "2026-04-08T14:30:00Z"
}
```

Retourne `null` si aucun PlanningPeriod n'existe pour ce département/mois/année.

