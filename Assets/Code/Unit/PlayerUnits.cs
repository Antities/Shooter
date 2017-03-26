using System.Collections.Generic;
using UnityEngine;
using TAMKShooter.Data;
using TAMKShooter.Systems;
using System;

namespace TAMKShooter
{
	public class PlayerUnits : MonoBehaviour
	{
		private Dictionary<PlayerData.PlayerId, PlayerUnit> _players =
			new Dictionary<PlayerData.PlayerId, PlayerUnit> ();

		public void Init(params PlayerData[] players)
		{
			foreach (PlayerData playerData in players)
			{
				// Get prefab by UnitType
				PlayerUnit unitPrefab =
					Global.Instance.Prefabs.
					GetPlayerUnitPrefab ( playerData.UnitType );

				if(unitPrefab != null)
				{
					// Initialize unit
					PlayerUnit unit = Instantiate ( unitPrefab, transform );
                    switch (playerData.Id)
                    {
                        case PlayerData.PlayerId.Player1:
                            {
                                unit.transform.position = Global.Instance.SpawnPoints[0].transform.position;
                                break;
                            }

                        case PlayerData.PlayerId.Player2:
                            {

                                unit.transform.position = Global.Instance.SpawnPoints[1].transform.position;
                                break;
                            }

                        case PlayerData.PlayerId.Player3:
                            {

                                unit.transform.position = Global.Instance.SpawnPoints[2].transform.position;
                                break;
                            }

                        case PlayerData.PlayerId.Player4:
                            {
                                unit.transform.position = Global.Instance.SpawnPoints[3].transform.position;
                                break;
                            }
                    }
					unit.transform.rotation = Quaternion.identity;
					unit.Init ( playerData );

					// Add player to dictionary
					_players.Add ( playerData.Id, unit );
				}
				else
				{
					Debug.LogError ( "Unit prefab with type " + playerData.UnitType +
						" could not be found!" );
				}
			}
		}

		public void UpdateMovement ( InputManager.ControllerType controller, 
			Vector3 input, bool shoot )
		{
			PlayerUnit playerUnit = null;
			foreach (var player in _players)
			{
				if(player.Value.Data.Controller == controller)
				{
					playerUnit = player.Value;
				}
			}

			if(playerUnit != null)
			{
				playerUnit.HandleInput ( input, shoot );
			}
		}

	}
}

