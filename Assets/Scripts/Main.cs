using Asteroids;
using Asteroids.Characteristics;
using Asteroids.Common;
using Asteroids.Management;
using Asteroids.PlayerData;
using Asteroids.Tech;
using Asteroids.Tech.Input.Intefaces;
using Asteroids.Tech.PlayerLoop;
using Tech.Input;
using UnityEngine;

public class Main : MonoBehaviour
{
    #region Private data

    [SerializeField] private PlayerData playerData;
    [SerializeField] private InputListener inputListener;
    private IInputTranslator inputTranslator;
    private IUpdatable playerLoopProcessor;

    #endregion

    
    #region Unity events

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        playerLoopProcessor.Update(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        playerLoopProcessor.FixedUpdate(Time.fixedDeltaTime);
    }

    private void LateUpdate()
    {
        playerLoopProcessor.LateUpdate(Time.deltaTime);
    }

    #endregion

    private void Initialize()
    {
        inputTranslator = new InputTranslator();
        playerLoopProcessor = new PlayerLoopProcessor();
        inputTranslator.Initialize(playerLoopProcessor as IPlayerLoopProcessor);
        inputListener.Initialize(inputTranslator);
        
        ServiceLocator.SetService(inputTranslator);
        ServiceLocator.SetService(inputListener as IInputListener);
        ServiceLocator.SetService(playerLoopProcessor as IPlayerLoopProcessor);
        ServiceLocator.SetService(new TransformRegistry() as ITransformRegistry);
        InitializePlayer();
    }

    private void InitializePlayer()
    {
        var playerShipGO = Instantiate(playerData.PlayerShipTransform);
        var playerTransform = playerShipGO.transform;
        var shipMarkUp = playerShipGO.GetComponent<ShipMarkUp>();
        
        var moveTransform = new AccelerationMove(playerTransform, playerData.MoveData.InitialSpeed, playerData.MoveData.Acceleration);
        var rotation = new RotationShip(playerTransform);
        var shipShoot = new ShipShoot(shipMarkUp.Barrels, playerData.ShootData.Bullet, playerData.ShootData.ShootForce);
        
        var playerHP = new Stat(new MinMaxCurrent(0, playerData.PlayerHp, playerData.PlayerHp));
        var ship = new Ship(moveTransform, rotation, shipShoot,playerTransform, playerHP, shipMarkUp);
        
        var player = new Player();
        player.Initialize(ship, Camera.main);
    }
}
