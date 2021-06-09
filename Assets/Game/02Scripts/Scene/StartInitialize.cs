/* *************************************************
* MainSceneUI InGame �� UI�\��
************************************************* */
namespace MainForce
{
	using UnityEngine;
	using UnityEngine.SceneManagement;

	public class StartInitialize : MonoBehaviour
	{
		[RuntimeInitializeOnLoadMethod()]
		static void Init()
		{
			SceneManager.LoadScene("MainSceneUI", LoadSceneMode.Additive);
			SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
		}
	}
}