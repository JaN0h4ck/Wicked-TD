using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerController : MonoBehaviour
{
    [Header("Tilemaps")]
    [SerializeField] private Grid m_grid;
    [SerializeField] private Tilemap m_interactiveMap;
    [Header("Tiles")]
    [SerializeField] private Tile m_hoverTile;
    [Header("Events")]
    [SerializeField] private Utils.Events.GameEvent onOpenTowerMenue;

    private Vector3Int m_previousMousePosition;
    private float m_offsetOfPrefabToTile = 0.5f;

    [Space]
    [SerializeField] private GameObject[] m_towerPrefabs;

    private void Update()
    {
        Vector3Int currentMousePosition = GetMousePosition();

        //Hover effect
        if (!currentMousePosition.Equals(m_previousMousePosition))
        {
            //Reset the old tile
            m_interactiveMap.SetTile(m_previousMousePosition, null);
            //Set the new tile
            m_interactiveMap.SetTile(currentMousePosition, m_hoverTile);
            //Set the new mouse position
            m_previousMousePosition = currentMousePosition;
        }

        //Interaction
        if (Input.GetMouseButtonDown(0))
        {

            Vector2 start = new Vector2(m_previousMousePosition.x + m_offsetOfPrefabToTile,
                                        m_previousMousePosition.y + m_offsetOfPrefabToTile);
            Vector2 direction = new Vector2(0, 0);
            //Tower spawn
            if (!Physics2D.Raycast(start, direction, 0.5f))
            {
                SpawnTower(m_towerPrefabs[UnityEngine.Random.Range(0, m_towerPrefabs.Length)]);
            }
            else
            {//Tower menue
                onOpenTowerMenue.Invoke();
            }
        }

        //Destroy Tower
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 start = new Vector2(m_previousMousePosition.x + m_offsetOfPrefabToTile,
                                        m_previousMousePosition.y + m_offsetOfPrefabToTile);
            Vector2 direction = new Vector2(0, 0);
            RaycastHit2D hit = Physics2D.Raycast(start, direction, m_offsetOfPrefabToTile);
            if (hit)
            {
                DestroyTower(hit.collider.gameObject);
            }
        }
    }

    private Vector3Int GetMousePosition()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return m_grid.WorldToCell(mouseWorldPosition);
    }

    public void SpawnTower(GameObject tower)
    {
        GameObject instantiatedTower = Instantiate(tower);

        Vector3 newPosition = m_previousMousePosition;
        newPosition.x += m_offsetOfPrefabToTile;
        newPosition.y += m_offsetOfPrefabToTile;

        instantiatedTower.transform.position = newPosition;
    }

    public void DestroyTower(GameObject tower)
    {
        Destroy(tower);
    }
}