// Vertex-colored unlit shader v1.0

// Author: Bishop Myers

// Last modified: October 7, 2014

Shader "Custom/VertexColorUnlit" {

	Properties {
		_Alpha ("Alpha", Range(0,1)) = 1.0
	}



	Category {

		Tags {"RenderType"="Transparent" "Queue"="Transparent"}

		Lighting Off

		BindChannels {

			Bind "Color", color

			Bind "Vertex", vertex

			Bind "TexCoord", texcoord

		}

		

		SubShader {

			Pass {

			}

		}

	}

}