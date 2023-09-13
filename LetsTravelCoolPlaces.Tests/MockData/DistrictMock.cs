namespace LetsTravelCoolPlaces.Tests.MockData
{
    public class DistrictMock
    {
        public async Task<List<District>> GetDistrict()
        {
            return JsonConvert.DeserializeObject<List<District>>("[{\"id\":\"1\",\"division_id\":\"3\",\"name\":\"Dhaka\",\"bn_name\":\"ঢাকা\",\"lat\":\"23.7115253\",\"long\":\"90.4111451\"},{\"id\":\"2\",\"division_id\":\"3\",\"name\":\"Faridpur\",\"bn_name\":\"ফরিদপুর\",\"lat\":\"23.6070822\",\"long\":\"89.8429406\"}]");
        }
    }
}