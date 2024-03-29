﻿namespace LetsTravelCoolPlaces.Tests.MockData
{
    public class DistrictMock
    {
        public List<District>? GetDistrict()
        {
            return JsonConvert.DeserializeObject<List<District>>("[{\"id\":\"19\",\"division_id\":\"5\",\"name\":\"Joypurhat\",\"bn_name\":\"জয়পুরহাট\",\"lat\":\"25.0968\",\"long\":\"89.0227\"},{\"id\":\"20\",\"division_id\":\"5\",\"name\":\"Naogaon\",\"bn_name\":\"নওগাঁ\",\"lat\":\"24.7936\",\"long\":\"88.9318\"},{\"id\":\"26\",\"division_id\":\"6\",\"name\":\"Dinajpur\",\"bn_name\":\"দিনাজপুর\",\"lat\":\"25.6217061\",\"long\":\"88.6354504\"},{\"id\":\"27\",\"division_id\":\"6\",\"name\":\"Gaibandha\",\"bn_name\":\"গাইবান্ধা\",\"lat\":\"25.328751\",\"long\":\"89.528088\"},{\"id\":\"28\",\"division_id\":\"6\",\"name\":\"Kurigram\",\"bn_name\":\"কুড়িগ্রাম\",\"lat\":\"25.805445\",\"long\":\"89.636174\"},{\"id\":\"29\",\"division_id\":\"6\",\"name\":\"Lalmonirhat\",\"bn_name\":\"লালমনিরহাট\",\"lat\":\"25.9923\",\"long\":\"89.2847\"},{\"id\":\"30\",\"division_id\":\"6\",\"name\":\"Nilphamari\",\"bn_name\":\"নীলফামারী\",\"lat\":\"25.931794\",\"long\":\"88.856006\"},{\"id\":\"31\",\"division_id\":\"6\",\"name\":\"Panchagarh\",\"bn_name\":\"পঞ্চগড়\",\"lat\":\"26.3411\",\"long\":\"88.5541606\"},{\"id\":\"32\",\"division_id\":\"6\",\"name\":\"Rangpur\",\"bn_name\":\"রংপুর\",\"lat\":\"25.7558096\",\"long\":\"89.244462\"},{\"id\":\"33\",\"division_id\":\"6\",\"name\":\"Thakurgaon\",\"bn_name\":\"ঠাকুরগাঁও\",\"lat\":\"26.0336945\",\"long\":\"88.4616834\"}]");
        }
    }
}