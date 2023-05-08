// using System.Collections;
// using Bomberman.AI.StatesV2.ControllerV2;
// using UnityEngine;
// using Utils;
//
// namespace Bomberman.AI.StatesV2
// {
//     public class Move : IState
//     {
//         private AIControllerV2 aiControllerV2;
//         private Vector3 lastPosition = Vector3.zero;
//         public float TimeStuck;
//
//         public Move(AIControllerV2 aiControllerV2)
//         {
//             this.aiControllerV2 = aiControllerV2;
//         }
//
//         public void Tick()
//         {
//             aiControllerV2.isMoving = false;
//             
//             if (Vector3.Distance(aiControllerV2.transform.position, lastPosition) <= 0f)
//                 TimeStuck += Time.deltaTime;
//
//             lastPosition = aiControllerV2.transform.position;
//             
//             float step =
//                 (aiControllerV2.gameObject.GetComponent<FinalBombermanStats>()
//                     .GetNumericStat(Stats.Speed)) * Time.deltaTime * 0.9f;
//                     
//             aiControllerV2.transform.LookAt(aiControllerV2.nextCell);
//             aiControllerV2.transform.Rotate(0, 180, 0);
//                     
//             aiControllerV2.transform.position =
//                 Vector3.MoveTowards(aiControllerV2.transform.position
//                     ,aiControllerV2.nextCell, step);
//
//             // yield return new WaitForSeconds(3f);
//             if (Vector3.Distance(aiControllerV2.transform.position,
//                     aiControllerV2.nextCell) < 0.01f)
//             {
//                 aiControllerV2.isMoving = true;
//             }
//         }
//
//         public void OnEnter()
//         {
//             TimeStuck = 0f;
//             aiControllerV2.isMoving = false;
//             var speed = aiControllerV2.GetComponent<FinalBombermanStats>().GetNumericStat(Stats.Speed);
//             
//             Debug.Log("Moving");
//         }
//         
//         public void OnExit()
//         {
//             aiControllerV2.isMoving = true;
//         }
//     }
// }