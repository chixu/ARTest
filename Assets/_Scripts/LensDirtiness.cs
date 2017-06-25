using UnityEngine;

[RequireComponent(typeof (Camera)), ExecuteInEditMode, AddComponentMenu("Image Effects/Lens Dirtiness")]
public class LensDirtiness : MonoBehaviour
{
    public Color BloomColor = Color.white;
    public float BloomSize = 5f;
    public float Dirtiness = 1f;
    public Texture2D DirtinessTexture;
    private Material Material_Dirtiness;
    private RenderTexture RTT_1;
    private RenderTexture RTT_2;
    private RenderTexture RTT_3;
    private RenderTexture RTT_4;
    private RenderTexture RTT_BloomThreshold;
    private RenderTexture RTT_Bloom_1;
    private RenderTexture RTT_Bloom_2;
    private RenderTextureFormat RTT_Format;
    public bool SceneTintsBloom = true;
    private int ScreenX = 0x500;
    private int ScreenY = 720;
    private Shader Shader_Dirtiness;
    public bool ShowScreenControls;
    public float gain = 1f;
    public float threshold = 1f;

    private void OnEnable()
    {
        Shader_Dirtiness = Shader.Find("Hidden/LensDirtiness");
        if (Shader_Dirtiness == null)
        {
            Debug.Log("#ERROR# LensDirtiness Shader not found");
        }
        Material_Dirtiness = new Material(Shader_Dirtiness) {hideFlags = HideFlags.HideAndDontSave};
        TextureFormat();
        SeyKeyword();
    }

    private void OnGUI()
    {
        if (ShowScreenControls)
        {
            float left = 150f;
            GUI.Box(new Rect(15f, 15f, 250f, 200f), string.Empty);
            GUI.Label(new Rect(25f, 25f, 100f, 20f), "Gain= " + gain.ToString("0.0"));
            gain = GUI.HorizontalSlider(new Rect(left, 30f, 100f, 20f), gain, 0f, 10f);
            GUI.Label(new Rect(25f, 45f, 100f, 20f), "Threshold= " + threshold.ToString("0.0"));
            threshold = GUI.HorizontalSlider(new Rect(left, 50f, 100f, 20f), threshold, 0f, 10f);
            GUI.Label(new Rect(25f, 65f, 100f, 20f), "BloomSize= " + BloomSize.ToString("0.0"));
            BloomSize = GUI.HorizontalSlider(new Rect(left, 70f, 100f, 20f), BloomSize, 0f, 10f);
            GUI.Label(new Rect(25f, 85f, 100f, 20f), "Dirtiness= " + Dirtiness.ToString("0.0"));
            Dirtiness = GUI.HorizontalSlider(new Rect(left, 90f, 100f, 20f), Dirtiness, 0f, 10f);
            GUI.Label(new Rect(25f, 125f, 100f, 20f), "R= " + ((BloomColor.r*255f)).ToString("0."));
            GUI.color = new Color(BloomColor.r, 0f, 0f);
            BloomColor.r = GUI.HorizontalSlider(new Rect(left, 130f, 100f, 20f), BloomColor.r, 0f, 1f);
            GUI.color = Color.white;
            GUI.Label(new Rect(25f, 145f, 100f, 20f), "G= " + ((BloomColor.g*255f)).ToString("0."));
            GUI.color = new Color(0f, BloomColor.g, 0f);
            BloomColor.g = GUI.HorizontalSlider(new Rect(left, 150f, 100f, 20f), BloomColor.g, 0f, 1f);
            GUI.color = Color.white;
            GUI.Label(new Rect(25f, 165f, 100f, 20f), "R= " + ((BloomColor.b*255f)).ToString("0."));
            GUI.color = new Color(0f, 0f, BloomColor.b);
            BloomColor.b = GUI.HorizontalSlider(new Rect(left, 170f, 100f, 20f), BloomColor.b, 0f, 1f);
            GUI.color = Color.white;
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        ScreenX = source.width;
        ScreenY = source.height;
        Material_Dirtiness.SetFloat("_Gain", gain);
        Material_Dirtiness.SetFloat("_Threshold", threshold);
        RTT_BloomThreshold = RenderTexture.GetTemporary(ScreenX/2, ScreenY/2, 0, RTT_Format);
        RTT_BloomThreshold.name = "RTT_BloomThreshold";
        Graphics.Blit(source, RTT_BloomThreshold, Material_Dirtiness, 0);
        Material_Dirtiness.SetVector("_Offset", new Vector4(1f/ScreenX, 1f/ScreenY, 0f, 0f)*2f);
        RTT_1 = RenderTexture.GetTemporary(ScreenX/2, ScreenY/2, 0, RTT_Format);
        Graphics.Blit(RTT_BloomThreshold, RTT_1, Material_Dirtiness, 1);
        RenderTexture.ReleaseTemporary(RTT_BloomThreshold);
        RTT_2 = RenderTexture.GetTemporary(ScreenX/4, ScreenY/4, 0, RTT_Format);
        Graphics.Blit(RTT_1, RTT_2, Material_Dirtiness, 1);
        RenderTexture.ReleaseTemporary(RTT_1);
        RTT_3 = RenderTexture.GetTemporary(ScreenX/8, ScreenY/8, 0, RTT_Format);
        Graphics.Blit(RTT_2, RTT_3, Material_Dirtiness, 1);
        RenderTexture.ReleaseTemporary(RTT_2);
        RTT_4 = RenderTexture.GetTemporary(ScreenX/0x10, ScreenY/0x10, 0, RTT_Format);
        Graphics.Blit(RTT_3, RTT_4, Material_Dirtiness, 1);
        RenderTexture.ReleaseTemporary(RTT_3);
        RTT_1.name = "RTT_1";
        RTT_2.name = "RTT_2";
        RTT_3.name = "RTT_3";
        RTT_4.name = "RTT_4";
        RTT_Bloom_1 = RenderTexture.GetTemporary(ScreenX/0x10, ScreenY/0x10, 0, RTT_Format);
        RTT_Bloom_1.name = "RTT_Bloom_1";
        RTT_Bloom_2 = RenderTexture.GetTemporary(ScreenX/0x10, ScreenY/0x10, 0, RTT_Format);
        RTT_Bloom_2.name = "RTT_Bloom_2";
        Graphics.Blit(RTT_4, RTT_Bloom_1);
        RenderTexture.ReleaseTemporary(RTT_4);
        for (int i = 1; i <= 8; i++)
        {
            float x = (BloomSize*i)/ScreenX;
            float y = (BloomSize*i)/ScreenY;
            Material_Dirtiness.SetVector("_Offset", new Vector4(x, y, 0f, 0f));
            Graphics.Blit(RTT_Bloom_1, RTT_Bloom_2, Material_Dirtiness, 1);
            Graphics.Blit(RTT_Bloom_2, RTT_Bloom_1, Material_Dirtiness, 1);
        }
        RenderTexture.ReleaseTemporary(RTT_Bloom_1);
        RenderTexture.ReleaseTemporary(RTT_Bloom_2);
        Material_Dirtiness.SetTexture("_Bloom", RTT_Bloom_2);
        Material_Dirtiness.SetFloat("_Dirtiness", Dirtiness);
        Material_Dirtiness.SetColor("_BloomColor", BloomColor);
        Material_Dirtiness.SetTexture("_DirtinessTexture", DirtinessTexture);
        Graphics.Blit(source, destination, Material_Dirtiness, 2);
    }

    private void SeyKeyword()
    {
        if (Material_Dirtiness != null)
        {
            if (SceneTintsBloom)
            {
                Material_Dirtiness.EnableKeyword("_SCENE_TINTS_BLOOM");
            }
            else
            {
                Material_Dirtiness.DisableKeyword("_SCENE_TINTS_BLOOM");
            }
        }
    }

    private void TextureFormat()
    {
        RTT_Format = Camera.main.hdr ? RenderTextureFormat.ARGBHalf : RenderTextureFormat.ARGB32;
    }

    private enum Pass
    {
        Threshold,
        Kawase,
        Compose
    }
}