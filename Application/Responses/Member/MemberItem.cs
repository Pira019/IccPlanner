namespace Application.Responses.Member
{
    public class MemberItem
    {
        /// <summary>
        ///     id du membre dans le département,
        /// </summary>
        public int IdDepartMember { get; set; }
        public string Name { get; set; }

        /// <summary>
        ///     Nom d'affichage du membre, qui peut être différent de son nom réel.
        /// </summary>
        public string NickName { get; set; }
        public string Sex { get; set; }

        /// <summary>
        ///     Statut du membre, qui peut indiquer s'il est actif, inactif, en attente, etc.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        ///     Postes associés au membre, qui peuvent indiquer les rôles ou les fonctions qu'il occupe dans le département.
        /// </summary>
        public List<string> Postes { get; set; }
    }
}
