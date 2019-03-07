using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0.15,0.15,0.15,0.15,0]")]
	public partial class PlayerNetworkObject : NetworkObject
	{
		public const int IDENTITY = 7;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		private Vector3 _position;
		public event FieldEvent<Vector3> positionChanged;
		public InterpolateVector3 positionInterpolation = new InterpolateVector3() { LerpT = 0.15f, Enabled = true };
		public Vector3 position
		{
			get { return _position; }
			set
			{
				// Don't do anything if the value is the same
				if (_position == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_position = value;
				hasDirtyFields = true;
			}
		}

		public void SetpositionDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_position(ulong timestep)
		{
			if (positionChanged != null) positionChanged(_position, timestep);
			if (fieldAltered != null) fieldAltered("position", _position, timestep);
		}
		private Quaternion _rotation;
		public event FieldEvent<Quaternion> rotationChanged;
		public InterpolateQuaternion rotationInterpolation = new InterpolateQuaternion() { LerpT = 0.15f, Enabled = true };
		public Quaternion rotation
		{
			get { return _rotation; }
			set
			{
				// Don't do anything if the value is the same
				if (_rotation == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x2;
				_rotation = value;
				hasDirtyFields = true;
			}
		}

		public void SetrotationDirty()
		{
			_dirtyFields[0] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_rotation(ulong timestep)
		{
			if (rotationChanged != null) rotationChanged(_rotation, timestep);
			if (fieldAltered != null) fieldAltered("rotation", _rotation, timestep);
		}
		private float _animhor;
		public event FieldEvent<float> animhorChanged;
		public InterpolateFloat animhorInterpolation = new InterpolateFloat() { LerpT = 0.15f, Enabled = true };
		public float animhor
		{
			get { return _animhor; }
			set
			{
				// Don't do anything if the value is the same
				if (_animhor == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x4;
				_animhor = value;
				hasDirtyFields = true;
			}
		}

		public void SetanimhorDirty()
		{
			_dirtyFields[0] |= 0x4;
			hasDirtyFields = true;
		}

		private void RunChange_animhor(ulong timestep)
		{
			if (animhorChanged != null) animhorChanged(_animhor, timestep);
			if (fieldAltered != null) fieldAltered("animhor", _animhor, timestep);
		}
		private float _animvert;
		public event FieldEvent<float> animvertChanged;
		public InterpolateFloat animvertInterpolation = new InterpolateFloat() { LerpT = 0.15f, Enabled = true };
		public float animvert
		{
			get { return _animvert; }
			set
			{
				// Don't do anything if the value is the same
				if (_animvert == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x8;
				_animvert = value;
				hasDirtyFields = true;
			}
		}

		public void SetanimvertDirty()
		{
			_dirtyFields[0] |= 0x8;
			hasDirtyFields = true;
		}

		private void RunChange_animvert(ulong timestep)
		{
			if (animvertChanged != null) animvertChanged(_animvert, timestep);
			if (fieldAltered != null) fieldAltered("animvert", _animvert, timestep);
		}
		private int _animstate;
		public event FieldEvent<int> animstateChanged;
		public Interpolated<int> animstateInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int animstate
		{
			get { return _animstate; }
			set
			{
				// Don't do anything if the value is the same
				if (_animstate == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x10;
				_animstate = value;
				hasDirtyFields = true;
			}
		}

		public void SetanimstateDirty()
		{
			_dirtyFields[0] |= 0x10;
			hasDirtyFields = true;
		}

		private void RunChange_animstate(ulong timestep)
		{
			if (animstateChanged != null) animstateChanged(_animstate, timestep);
			if (fieldAltered != null) fieldAltered("animstate", _animstate, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			positionInterpolation.current = positionInterpolation.target;
			rotationInterpolation.current = rotationInterpolation.target;
			animhorInterpolation.current = animhorInterpolation.target;
			animvertInterpolation.current = animvertInterpolation.target;
			animstateInterpolation.current = animstateInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _position);
			UnityObjectMapper.Instance.MapBytes(data, _rotation);
			UnityObjectMapper.Instance.MapBytes(data, _animhor);
			UnityObjectMapper.Instance.MapBytes(data, _animvert);
			UnityObjectMapper.Instance.MapBytes(data, _animstate);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_position = UnityObjectMapper.Instance.Map<Vector3>(payload);
			positionInterpolation.current = _position;
			positionInterpolation.target = _position;
			RunChange_position(timestep);
			_rotation = UnityObjectMapper.Instance.Map<Quaternion>(payload);
			rotationInterpolation.current = _rotation;
			rotationInterpolation.target = _rotation;
			RunChange_rotation(timestep);
			_animhor = UnityObjectMapper.Instance.Map<float>(payload);
			animhorInterpolation.current = _animhor;
			animhorInterpolation.target = _animhor;
			RunChange_animhor(timestep);
			_animvert = UnityObjectMapper.Instance.Map<float>(payload);
			animvertInterpolation.current = _animvert;
			animvertInterpolation.target = _animvert;
			RunChange_animvert(timestep);
			_animstate = UnityObjectMapper.Instance.Map<int>(payload);
			animstateInterpolation.current = _animstate;
			animstateInterpolation.target = _animstate;
			RunChange_animstate(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _position);
			if ((0x2 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _rotation);
			if ((0x4 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _animhor);
			if ((0x8 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _animvert);
			if ((0x10 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _animstate);

			// Reset all the dirty fields
			for (int i = 0; i < _dirtyFields.Length; i++)
				_dirtyFields[i] = 0;

			return dirtyFieldsData;
		}

		protected override void ReadDirtyFields(BMSByte data, ulong timestep)
		{
			if (readDirtyFlags == null)
				Initialize();

			Buffer.BlockCopy(data.byteArr, data.StartIndex(), readDirtyFlags, 0, readDirtyFlags.Length);
			data.MoveStartIndex(readDirtyFlags.Length);

			if ((0x1 & readDirtyFlags[0]) != 0)
			{
				if (positionInterpolation.Enabled)
				{
					positionInterpolation.target = UnityObjectMapper.Instance.Map<Vector3>(data);
					positionInterpolation.Timestep = timestep;
				}
				else
				{
					_position = UnityObjectMapper.Instance.Map<Vector3>(data);
					RunChange_position(timestep);
				}
			}
			if ((0x2 & readDirtyFlags[0]) != 0)
			{
				if (rotationInterpolation.Enabled)
				{
					rotationInterpolation.target = UnityObjectMapper.Instance.Map<Quaternion>(data);
					rotationInterpolation.Timestep = timestep;
				}
				else
				{
					_rotation = UnityObjectMapper.Instance.Map<Quaternion>(data);
					RunChange_rotation(timestep);
				}
			}
			if ((0x4 & readDirtyFlags[0]) != 0)
			{
				if (animhorInterpolation.Enabled)
				{
					animhorInterpolation.target = UnityObjectMapper.Instance.Map<float>(data);
					animhorInterpolation.Timestep = timestep;
				}
				else
				{
					_animhor = UnityObjectMapper.Instance.Map<float>(data);
					RunChange_animhor(timestep);
				}
			}
			if ((0x8 & readDirtyFlags[0]) != 0)
			{
				if (animvertInterpolation.Enabled)
				{
					animvertInterpolation.target = UnityObjectMapper.Instance.Map<float>(data);
					animvertInterpolation.Timestep = timestep;
				}
				else
				{
					_animvert = UnityObjectMapper.Instance.Map<float>(data);
					RunChange_animvert(timestep);
				}
			}
			if ((0x10 & readDirtyFlags[0]) != 0)
			{
				if (animstateInterpolation.Enabled)
				{
					animstateInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					animstateInterpolation.Timestep = timestep;
				}
				else
				{
					_animstate = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_animstate(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (positionInterpolation.Enabled && !positionInterpolation.current.UnityNear(positionInterpolation.target, 0.0015f))
			{
				_position = (Vector3)positionInterpolation.Interpolate();
				//RunChange_position(positionInterpolation.Timestep);
			}
			if (rotationInterpolation.Enabled && !rotationInterpolation.current.UnityNear(rotationInterpolation.target, 0.0015f))
			{
				_rotation = (Quaternion)rotationInterpolation.Interpolate();
				//RunChange_rotation(rotationInterpolation.Timestep);
			}
			if (animhorInterpolation.Enabled && !animhorInterpolation.current.UnityNear(animhorInterpolation.target, 0.0015f))
			{
				_animhor = (float)animhorInterpolation.Interpolate();
				//RunChange_animhor(animhorInterpolation.Timestep);
			}
			if (animvertInterpolation.Enabled && !animvertInterpolation.current.UnityNear(animvertInterpolation.target, 0.0015f))
			{
				_animvert = (float)animvertInterpolation.Interpolate();
				//RunChange_animvert(animvertInterpolation.Timestep);
			}
			if (animstateInterpolation.Enabled && !animstateInterpolation.current.UnityNear(animstateInterpolation.target, 0.0015f))
			{
				_animstate = (int)animstateInterpolation.Interpolate();
				//RunChange_animstate(animstateInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public PlayerNetworkObject() : base() { Initialize(); }
		public PlayerNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public PlayerNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
