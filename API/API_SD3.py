from flask import Flask, request, jsonify
from gradio_client import Client

app = Flask(__name__)
client = Client("stabilityai/stable-diffusion-3-medium")

@app.route('/')
def home():
    return "API is working!"

@app.route('/predict', methods=['POST'])
def predict():
    data = request.json
    result = client.predict(
        prompt=data.get("prompt"),
        negative_prompt=data.get("negative_prompt"),
        seed=data.get("seed"),
        randomize_seed=data.get("randomize_seed"),
        width=data.get("width"),
        height=data.get("height"),
        guidance_scale=data.get("guidance_scale"),
        num_inference_steps=data.get("num_inference_steps")
    )
    return jsonify(result)

if __name__ == '__main__':
    app.run(debug=True)
