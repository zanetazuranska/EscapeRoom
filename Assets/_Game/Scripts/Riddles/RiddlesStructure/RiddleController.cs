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
        [SerializeField] private bool _spawnNetworkObject = true;
        private Transform _riddleActivateObj;

        public UnityEvent<Transform> OnObjectSpawn = new UnityEvent<Transform>();
        public UnityEvent OnAnswerCorrectEvent = new UnityEvent();

        private void Awake()
        {
            EscapeRoomApp.Instance.OnClientSpawned.AddListener(OnClientSpawned);
            EscapeRoomApp.Instance.OnHostSpawned.AddListener(OnHostSpawned);
        }

        private void Start()
        {
            _riddleLogic.LogicInitialize();
        }

        private void OnHostSpawned()
        {
            EscapeRoomApp.Instance.OnHostSpawned.RemoveListener(OnHostSpawned);

            EscapeRoomApp.Instance.GetAplicationFlowController().AddRiddleController(this);

            if(_spawnNetworkObject)
            {
                _riddleActivateObj = Instantiate(_riddleActivateObjPrefab, this.gameObject.transform);
                _riddleActivateObj.GetComponent<NetworkObject>().Spawn(true);
            }
            else
            {
                _riddleActivateObj = _riddleActivateObjPrefab;
            }

            OnObjectSpawn.Invoke(_riddleActivateObj);
        }

        private void OnClientSpawned()
        {
            EscapeRoomApp.Instance.OnClientSpawned.RemoveListener(OnClientSpawned);

            EscapeRoomApp.Instance.GetAplicationFlowController().AddRiddleController(this);

            OnObjectSpawn.Invoke(_riddleActivateObj);
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

            OnAnswerCorrectEvent.Invoke();
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
            EscapeRoomApp.Instance.OnHostSpawned.RemoveListener(OnHostSpawned);
            EscapeRoomApp.Instance.OnClientSpawned.RemoveListener(OnClientSpawned);
        }
    }
}

