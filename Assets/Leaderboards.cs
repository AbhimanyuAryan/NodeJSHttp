using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
 
public class Leaderboards : MonoBehaviour
{
	public InputField whichField;
	public InputField scoreField;

    private static string _Url = "http://localhost";
    private static string _Port = "5001";
 
    public void SubmitScore()
    {
		int which = int.Parse(whichField.text);
		int score = int.Parse(scoreField.text); 
        StartCoroutine(SubmitScoreToServer(which, score));
    }
 
    private IEnumerator SubmitScoreToServer(int which, int score)
    {
        Debug.Log("Submitting score");
 
        // Create a form that will contain our data
        WWWForm form = new WWWForm();
        form.AddField("which", which.ToString());
        form.AddField("score", score.ToString());
 
        // Create a POST web request with our form data
        UnityWebRequest www = UnityWebRequest.Post(_Url + ":" + _Port, form);
        // Send the request and yield until the send completes
        yield return www.Send();
 
        if (www.isError)
        {
            // There was an error
            Debug.Log(www.error);
        }
        else
        {
            if (www.responseCode == 200)
            {
                // Response code 200 signifies that the server had no issues with the data we went
                Debug.Log("Form sent complete!");
                Debug.Log("Response:" + www.downloadHandler.text);
            }
            else
            {
                // Any other response signifies that there was an issue with the data we sent
                Debug.Log("Error response code:" + www.responseCode.ToString());
            }
        }
    }
}