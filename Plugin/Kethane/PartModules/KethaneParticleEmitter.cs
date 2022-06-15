using System;
using System.Linq;
using UnityEngine;

using KethaneParticles;

namespace Kethane.PartModules
{
    public class KethaneParticleEmitter : PartModule
    {
		ParticleManager.Emitter emitter;

		bool bad;
        public bool Emit
        {
            get {
				if (emitter != null) {
					return emitter.enabled;
				}
				return false;
			}
            set {
				if (!bad && value && emitter == null) {
					emitter = ParticleManager.CreateEmitter (System);
					if (emitter == null) {
						bad = true;
						return;
					}
					emitter.position = position;
					emitter.direction = direction;
					Utils.CalcAxes (emitter.direction, out emitter.spreadX, out emitter.spreadY);
					emitter.spreadX *= spread.x;
					emitter.spreadY *= spread.y;
				}
				if (emitter != null) {
					emitter.enabled = value;
				}
			}
        }

		public float Rate
		{
			get {
				if (emitter != null) {
					return emitter.rate;
				}
				return 0;
			}
			set {
				if (emitter == null) {
					Emit = false;
				}
				if (emitter != null) {
					emitter.rate = value;
				}
			}
		}

		public Vector3 Position
		{
			get {
				if (emitter != null) {
					return emitter.position;
				}
				return position;
			}
			set {
				if (emitter == null) {
					Emit = false;
				}
				if (emitter != null) {
					emitter.position = value;
				}
				position = value;
			}
		}

		public Vector3 Direction
		{
			get {
				if (emitter != null) {
					return emitter.direction;
				}
				return Vector3.up;
			}
			set {
				if (emitter == null) {
					Emit = false;
				}
				if (emitter != null) {
					emitter.direction = value;
				}
			}
		}

        [KSPField(isPersistant = false)]
        public string Label;

        [KSPField(isPersistant = false)]
		public string System;

        [KSPField(isPersistant = false)]
		public Vector3 position;

        [KSPField(isPersistant = false)]
		public Vector3 direction = Vector3.up;

        [KSPField(isPersistant = false)]
		public Vector2 spread;

        public override void OnLoad(ConfigNode config)
        {
        }

        public override void OnSave(ConfigNode config)
        {
        }

        public override void OnStart(StartState state)
        {
            if (!HighLogic.LoadedSceneIsFlight) { return; }
        }
    }
}
