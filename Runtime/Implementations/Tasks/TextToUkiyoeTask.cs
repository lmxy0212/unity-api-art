using Newtonsoft.Json.Linq;
using UnityEngine;

namespace HuggingFace.API {
    public class TextToUkiyoeTask : TaskBase<string, Texture2D> {
        // Set the task name and specify the model endpoint for Ukiyo-e diffusion
        public override string taskName => "TextToUkiyoe";
        public override string defaultEndpoint => "https://api-inference.huggingface.co/models/SakanaAI/Evo-Ukiyoe-v1";

        // Optionally, add backup endpoints if available
        protected override string[] LoadBackupEndpoints() {
            return new string[] {
                "https://api-inference.huggingface.co/models/CompVis/stable-diffusion-v1-4",
                "https://api-inference.huggingface.co/models/prompthero/openjourney"
            };
        }

        // Define the payload for the request, sending the prompt as input
        protected override IPayload GetPayload(string input, object context) {
            return new JObjectPayload(new JObject {
                ["inputs"] = input
            });
        }

        // Handle the response, converting raw data to a Texture2D image
        protected override bool PostProcess(object raw, string input, object context, out Texture2D response, out string error) {
            response = new Texture2D(2, 2);
            if (response.LoadImage(raw as byte[])) {
                error = null;
                return true;
            } else {
                error = "Failed to load image.";
                return false;
            }
        }
    }
}
