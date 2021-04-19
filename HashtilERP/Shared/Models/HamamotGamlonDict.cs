using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashtilERP.Shared.Models
{
    public static class HamamotGamlonDict
    {
        public  static Dictionary<int,List<int>> HamamaGamlonDictInit()
        {
            Dictionary<int, List<int>> hamamaGamlon = new Dictionary<int, List<int>>();
            hamamaGamlon.Add(1,new List<int> {5, 6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34 });
            hamamaGamlon.Add(2, new List<int> {1,2,3,4,5, 6, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22});
            hamamaGamlon.Add(3, new List<int> { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22 });
            hamamaGamlon.Add(4, new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28 });
            hamamaGamlon.Add(5, new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18});
            hamamaGamlon.Add(6, new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28 });
            hamamaGamlon.Add(7, new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32 });

            return hamamaGamlon;

        }
    }
}
