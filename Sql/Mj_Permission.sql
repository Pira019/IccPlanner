/*
    Permet de cr�er une permission si elle n'existe pas d�j�.
*/ 
CREATE OR REPLACE PROCEDURE Mj_Permissions(
    p_name 	text ,
    p_description text 
)
LANGUAGE plpgsql
AS $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM Permissions WHERE Name = p_name
    ) THEN
        INSERT INTO permissions (Name, Description) VALUES (p_name, p_description);
    END IF;
END;
$$;  
