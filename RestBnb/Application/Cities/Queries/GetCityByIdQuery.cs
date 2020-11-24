namespace RestBnb.API.Application.Cities.Queries
{
    public class GetCityByIdQuery
    {
        public int Id { get; set; }

        public GetCityByIdQuery(int id)
        {
            Id = id;
        }
    }
}
