using Newtonsoft.Json.Linq;
using UnityEngine;

namespace HuggingFace.API {
    public class TextToUkiyoeTask : TaskBase<string, Texture2D> {
        public override string taskName => "TextToUkiyoe";
        public override string defaultEndpoint => "https://api-inference.huggingface.co/models/SakanaAI/Evo-Ukiyoe-v1";

        protected override string[] LoadBackupEndpoints() {
            return new string[] {
                "https://api-inference.huggingface.co/models/another-backup-model" // Add backup model if available
            };
        }

        protected override IPayload GetPayload(string input, object context) {
            return new JObjectPayload(new JObject {
                ["inputs"] = input
            });
        }

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
