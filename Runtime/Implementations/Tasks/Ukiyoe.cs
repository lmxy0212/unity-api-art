using System.Threading.Tasks;
using UnityEngine;
using HuggingFace.API;
using HuggingFace.Client;

public class TextToUkiyoeTask : TextToImageTask
{
    // Override the model ID with the Ukiyo-e Diffusion model
    protected override string ModelId => "SakanaAI/Evo-Ukiyoe-v1";

    // Custom prompt example (you can customize the prompt as needed)
    public string prompt = "Astronaut in a jungle, cold color palette, muted colors, detailed, 8k";

    // Initialize the task with default parameters
    public UkiyoeDiffusionTask(string prompt = null)
    {
        if (!string.IsNullOrEmpty(prompt))
        {
            this.prompt = prompt;
        }
    }

    // Method to perform the Ukiyo-e style image generation
    public async Task<Texture2D> GenerateImage()
    {
        // Set up the Hugging Face API client and authentication (ensure your API key is set)
        var client = new HuggingFaceClient(new HuggingFaceSettings { ApiToken = "YOUR_API_TOKEN" });

        // Create a text-to-image request with your prompt
        var request = new TextToImageRequest
        {
            Inputs = prompt,
            Model = ModelId
        };

        // Send request and await response
        var response = await client.TextToImageAsync(request);

        if (response.IsSuccess)
        {
            // Convert the resulting image into a Unity Texture2D
            var imageBytes = response.Result;
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageBytes);
            return texture;
        }
        else
        {
            Debug.LogError("Image generation failed: " + response.Error);
            return null;
        }
    }
}
