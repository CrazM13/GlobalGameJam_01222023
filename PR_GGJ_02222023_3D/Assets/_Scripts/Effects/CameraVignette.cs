using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraVignette : ScriptableRendererFeature {

	private class CameraVignettePass : ScriptableRenderPass {
		private Material material;
		private Mesh mesh;

		public CameraVignettePass(Material material, Mesh mesh) {
			this.material = material;
			this.mesh = mesh;
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData) {
			CommandBuffer cmd = CommandBufferPool.Get(name: "CameraVignettePass");

			Camera camera = renderingData.cameraData.camera;

			//cmd.SetViewProjectionMatrices(Matrix4x4.identity, Matrix4x4.identity);

			cmd.DrawMesh(this.mesh, Matrix4x4.TRS(camera.transform.position + camera.transform.forward, camera.transform.rotation, new Vector3(3, 3, 1)), this.material, 0, 0);
			context.ExecuteCommandBuffer(cmd);

			CommandBufferPool.Release(cmd);
		}
	}

	public bool IsEffectActive {
		set {
			material.SetInt("_Active", value ? 1 : 0);
		}
	}

	public float Strength {
		set {
			material.SetFloat("_Strength", value);
		}
	}

	public float Scale {
		set {
			material.SetFloat("_Scale", value);
		}
	}

	[SerializeField] private Material material;
	[SerializeField] private Mesh mesh;

	private CameraVignettePass cameraVignettePass;

	public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData) {
		if (material != null && mesh != null) {
			renderer.EnqueuePass(cameraVignettePass);
		}
	}

	public override void Create() {
		cameraVignettePass = new CameraVignettePass(material, mesh);
	}
}
