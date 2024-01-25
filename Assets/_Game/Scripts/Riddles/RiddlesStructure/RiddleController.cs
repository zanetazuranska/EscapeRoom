using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace ER.Riddle
{
    public class RiddleController : NetworkBehaviour
    {
        [SerializeField] private RiddleLogic _riddleLogic;
        [SerializeField] private Transform _riddleActivateObjPrefab;
        private Transform _riddleActivateObj;

        public UnityEvent<Transform> OnObjectSpawn = new UnityEvent<Transform>();

        private void Awake()
        {
            EscapeRoomApp.Instance.OnHostSpawned.AddListener(OnHostSpawned);
            EscapeRoomApp.Instance.OnClientSpawned.AddListener(OnClientSpawned);
        }

        private void Start()
        {
            _riddleLogic.LogicInitialize();
        }

        private void OnHostSpawned()
        {
            EscapeRoomApp.Instance.GetAplicationFlowController().AddRiddleController(this);

            _riddleActivateObj = Instantiate(_riddleActivateObjPrefab, this.gameObject.transform);
            _riddleActivateObj.GetComponent<NetworkObject>().Spawn(true);

            OnObjectSpawn.Invoke(_riddleActivateObj);

            EscapeRoomApp.Instance.OnHostSpawned.RemoveListener(OnHostSpawned);
        }

        private void OnClientSpawned()
        {
            EscapeRoomApp.Instance.GetAplicationFlowController().AddRiddleController(this);

            OnObjectSpawn.Invoke(_riddleActivateObj);
            EscapeRoomApp.Instance.OnClientSpawned.RemoveListener(OnClientSpawned);
        }

        public bool IsAnswerCorrect(RiddleData riddleData)
        {
            if(_riddleLogic.CheckAnswer(riddleData))
            {
                OnAnswerCorrect();
                return true;
            }
            return false;
        }

        public RiddleData GetRiddleData()
        {
            return _riddleLogic.GetRiddleData();
        }

        public RiddleType.ERiddleType GetERiddleType()
        {
            return _riddleLogic.GetRiddleType();
        }

        public RiddleLogic GetRiddleLogic()
        {
            return _riddleLogic;
        }

        private void OnAnswerCorrect()
        {
            GetRiddleLogic().OnRiddleCorrect();

            RiddleCorrectServerRpc();
        }

        [ServerRpc(RequireOwnership = false)]
        private void RiddleCorrectServerRpc()
        {
            _riddleActivateObj.GetComponent<NetworkObject>().Despawn();
            Destroy(_riddleActivateObj.gameObject);
        }

        public Transform GetRiddleActiveGameObject()
        {
            return _riddleActivateObj;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            EscapeRoomApp.Instance.GetAplicationFlowController().RemoveRiddleController(this);
        }
    }
}

