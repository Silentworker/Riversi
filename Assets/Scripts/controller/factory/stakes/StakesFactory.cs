using Assets.Scripts.consts;
using Assets.Scripts.controller.behaviour.stake;
using Assets.Scripts.model.playfield;
using UnityEngine;

namespace Assets.Scripts.controller.factory.stakes
{
    public class StakesFactory : MonoBehaviour
    {
        public GameObject Folder;
        public GameObject StakePrefab;

        void Awake()
        {
            var halffield = Distance.PlayFieldSize * Distance.CellSize / 2;
            var cellSize = Distance.CellSize;
            var halfCellSize = cellSize / 2;

            for (var i = 0; i < Distance.PlayFieldSize; i++)
            {
                var topStake = Instantiate(StakePrefab);
                topStake.transform.SetParent(Folder.transform, false);
                topStake.transform.position = new Vector3(i * cellSize - halffield + halfCellSize, 0, halffield + halfCellSize);
                topStake.GetComponent<StakeText>().SetText(((char)(i + 65)).ToString());

                var bottomStake = Instantiate(StakePrefab);
                bottomStake.transform.SetParent(Folder.transform, false);
                bottomStake.transform.position = new Vector3(i * cellSize - halffield + halfCellSize, 0, -halffield - halfCellSize);
                bottomStake.GetComponent<StakeText>().SetText(((char)(i + 65)).ToString());

                var leftStake = Instantiate(StakePrefab);
                leftStake.transform.SetParent(Folder.transform, false);
                leftStake.transform.position = new Vector3(-halffield - halfCellSize, 0, i * cellSize - halffield + halfCellSize);
                leftStake.GetComponent<StakeText>().SetText((Distance.PlayFieldSize - (i + 1)).ToString());

                var rightStake = Instantiate(StakePrefab);
                rightStake.transform.SetParent(Folder.transform, false);
                rightStake.transform.position = new Vector3(halffield + halfCellSize, 0, i * cellSize - halffield + halfCellSize);
                rightStake.GetComponent<StakeText>().SetText((Distance.PlayFieldSize - (i + 1)).ToString());
            }
        }

    }
}
