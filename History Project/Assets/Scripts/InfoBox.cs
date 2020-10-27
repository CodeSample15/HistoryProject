using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.UI;

public class InfoBox : MonoBehaviour
{
    //public
    [SerializeField] public InfoController infoController;
    [SerializeField] public TextMeshProUGUI TextBox;
    [SerializeField] public Prompter prompter;
    public int type;

    //private
    private Button button;

    private List<string> infoList;
    private List<Vector2> locations;

    void Start()
    {
        init_info_data();
        init_marker_locations();
        TextBox.SetText(infoList[type]);

        transform.position = locations[type];
        button = GetComponent<Button>();
        button.onClick.AddListener(getInfo);

        TextBox.SetText("");
    }

    public void Update()
    {
        //Debug.Log(transform.position.x + "    " + transform.position.y);
    }

    private void init_info_data()
    {
        infoList = new List<string>();
        infoList.Add("Pacific Ocean - In 1493 the Spanish claimed the West Coast of North America due to the Papal Bull of 1493 which was when Pope Alexander VI asserted the right to colonize, convert, and enslave as well as the Treaty of Tordesillas. The claim was reinforced by Vasco Nunez de Balboa in 1513. He claimed all lands next to the Pacific Ocean for the Spanish Crown."); //Pacific ocean
        infoList.Add("Santo Domingo - In 1496 Bartholomew Columbus settled on the island and called it Hispanolia. It was the first Spanish colony set up by the Spaniards. Eventually African slaves would be brought to the colony. Present day the island consists of Haiti and the Dominican Republic. Santo Domingo was the capital of the Spanish colony."); //Santo Domingo
        infoList.Add("Havana - Havana was discovered by the Spaiards in 1509. In 1510 Spanish Colonists traveled from Hispaniola to Cuba to explore it. Diego Velaquez de Cueller founded Havana on August 25, 1514. Diego was a conquistador who played a large role during spanish colonization. Havana would later become to serve as an important port city to the Spanish."); //Havana
        infoList.Add("Tenochtitlan - In 1519 Hernan Cortez who was another Spanish conquistador set sail to Mexico. On August 13, 1521 Cortez conquered Tenochtitlan which marked the beginning of Spanish rule in Mexico. Cortez would make alliances with city states and rivals of the people of Tenochtitlan from other cities. Disease was also a massive killer when Cortez came to conquer new lands as the natives were not immune to diseases at all."); //Tenochtitlan
        infoList.Add("Cajamarca - Cajamarca is a city in Peru. The Battle of Cajamarca happened on November 16, 1532. A small Spanish force that was led by Francisco Pizarro ambushed the Incan ruler Atahualpa. The Spanish killed many of his people and marked the conquest of what is present day Peru. This led to the fall of the Incan empire. "); //Cajamarca
        infoList.Add("Cusco - Cusco is a city in Peru and used to be the capital of the Incan empire. The Spanish would start to colonize Cusco and would use the city as a main place for the spread and conversion of Christoianity. The Spanish would replace temples with churches and Incan palaces for mansions for the invaders. The Spanish also constructed things like cathedrals and cattle raising became important in this city."); //Cusco
        infoList.Add("La Paz - In 1548 the city of La Paz which is located in present day Bolivia was founded by Spanish settlers. The Spanish were interested in the gold that was abundant in the area which ran through the Choqueyapu River. The Spanish took away the Aymara peoples gold mines who lived in the area and forced them to work as slaves. La Paz would later serve as a great trade stop and become an agricultural area as well."); //La Paz
        infoList.Add("Potosi - Potosi is a city in Bolivia that is rich in silver mines. Because of this in 1672 Potosi became where many spanish mint coins were made. Potosi had a large population at the time and Spain used slaves to work in the silver mines which granted the Spanish a ton of wealth. Way later in 1825 Bolivia would declare independence but the Spanish had already taken advantage of most of the silver mines by this time which didn’t leave much wealth for bolivia."); //Potosi
    }

    private void init_marker_locations()
    {
        locations = new List<Vector2>();

        locations.Add(new Vector2(-5.5f, 2f)); //Pacific ocean
        locations.Add(new Vector2(0.7f, 0.5f)); //Santo Domingo
        locations.Add(new Vector2(-0.82f, 1.15f)); //Havana
        locations.Add(new Vector2(-3.01f, 0.93f)); //Tenochtitlan
        locations.Add(new Vector2(-0.29f, -2.57f)); //Cajamarca
        locations.Add(new Vector2(0.61f, -3.95f)); //Cusco
        locations.Add(new Vector2(0.95f, -4.24f)); //La Paz
        locations.Add(new Vector2(1.12f, -4.46f)); //Potosi
    }

    public void getInfo()
    {
        if (prompter.finished)
        {
            prompter.clear();
            StartCoroutine(showingInfo());
            TextBox.SetText(infoList[type]);
            TextBox.gameObject.transform.position = new Vector2(2.1f, 3.1f);
        }
    }

    IEnumerator showingInfo()
    {
        infoController.infoShowing = true;

        yield return new WaitForSeconds(0.5f);

        infoController.infoShowing = false;
    }
}