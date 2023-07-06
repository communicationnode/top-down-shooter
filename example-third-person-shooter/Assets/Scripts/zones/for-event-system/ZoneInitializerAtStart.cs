using UnityEngine;
using System.Collections.Generic;

namespace ExampleThirdPersonShooter.Zones
{
    [AddComponentMenu("Zones/Tools/Zone Initializer At Start")]
    public sealed class ZoneInitializerAtStart : MonoBehaviour
    {
        [SerializeField] private ZonePacket[] zonePackets;

        private void Update         ()
        {
            // статус генерации (успешная / испорченная)
            bool isCorruptedGenerate = false;
            // буфер с зонами
            List<GameObject> array = new List<GameObject>(8);


            // тут зоны создаются
            CreateZones(ref array);

            // тут проверка на испорченную генерацию
            CheckZones(ref array, ref isCorruptedGenerate);

            // тут пересоздается генерация, если она признана испорченной, либо компонент удаляется ввиду успешной генерации
            TryAppendZones(ref array, ref isCorruptedGenerate);

            // P.S: если генерация успешна, зоны активируются -> activated = true;
        }

        private void CreateZones    (ref List<GameObject> _list)
        {           
            for (byte packet = 0; packet < zonePackets.Length; packet++)
            {
                for (byte slot = 0; slot < zonePackets[packet].count; slot++)
                {
                    // инициализация зоны
                    GameObject zone = Instantiate(zonePackets[packet].zone, new Vector3(
                        Random.Range(-zonePackets[packet].generatorBounds.x / 2, zonePackets[packet].generatorBounds.x / 2),
                        Random.Range(-zonePackets[packet].generatorBounds.y / 2, zonePackets[packet].generatorBounds.y / 2),
                        Random.Range(-zonePackets[packet].generatorBounds.z / 2, zonePackets[packet].generatorBounds.z / 2)), Quaternion.identity);

                    // размеры зоны
                    zone.transform.localScale = new Vector3(zonePackets[packet].radius, zonePackets[packet].radius, zonePackets[packet].radius);

                    // записать в буфер на случай, если генерация будет признана испорченной
                    _list.Add(zone);
                }
            }
        }
        private void CheckZones     (ref List<GameObject> _list, ref bool _isCorruptedGenerate)
        {
            for (byte checker = 0; checker < _list.Count; checker++)
            {
                for (byte checking = 0; checking < _list.Count; checking++)
                {
                    if (_list[checker] == _list[checking]) continue;

                    // признать испорченным, если зона слишком близко к центру
                    if (Vector3.Distance(_list[checker].transform.position, Vector3.zero) < 6)
                    {
                        _isCorruptedGenerate = true;
                        break;
                    }

                    // признать испорченным, если зона ближе к другой зоне на 3 юнита
                    if (Vector3.Distance(_list[checker].transform.position, _list[checking].transform.position) < 3)
                    {
                        _isCorruptedGenerate = true;
                        break;
                    }
                }
            }
        }
        private void TryAppendZones (ref List<GameObject> _list, ref bool _isCorruptedGenerate)
        {
            // признать генерацию испорченной, очистить ее и завершить метод
            if (_isCorruptedGenerate)
            {
                for (byte slot = 0; slot < _list.Count; slot++)
                {
                    Destroy(_list[slot]);
                }
                _list.Clear();

                Debug.Log("Generation is corrupted");
            }

            // признать, что генерация не испорчена и завершить метод с успешной активацией всех зон
            else
            {
                // активация зон для дальнейшей работы
                for (byte slot = 0; slot < _list.Count; slot++)
                {
                    _list[slot].GetComponent<ZoneMonoBehaviour>().activated = true;
                }

                Destroy(this);
                _list.Clear();
                return;
            }
        }


        [System.Serializable]
        public sealed class ZonePacket
        {
            public GameObject   zone;
            public byte         count;
            public Vector3      generatorBounds;
            public float        radius;
        }
    }
}

