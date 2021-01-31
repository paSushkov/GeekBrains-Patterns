using Asteroids;
using Asteroids.Characteristics;
using Asteroids.Common;
using Asteroids.Management;
using Asteroids.PlayerData;
using Asteroids.Tech.PlayerLoop;
using UnityEngine;

public class Main : MonoBehaviour
{
    #region Private data

    [SerializeField] private PlayerData playerData;
    private IUpdatable playerLoopProcessor;
    private ITransformRegistry transformRegistry;
    private Player player;
    

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
        playerLoopProcessor.FixedUpdate(Time.deltaTime);
    }

    #endregion

    private void Initialize()
    {
        playerLoopProcessor = new PlayerLoopProcessor();
        transformRegistry = new TransformRegistry();
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
        var ship = new Ship(moveTransform, rotation, shipShoot,playerTransform, transformRegistry, playerHP, shipMarkUp);
        
        player = new Player();
        player.Initialize(playerLoopProcessor as PlayerLoopProcessor, ship, Camera.main);
    }
}
