﻿// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.DeviceSystem.PCI;

namespace Mosa.DeviceSystem
{
	/// <summary>
	/// Abstract class for hardware devices
	/// </summary>
	public abstract class DeviceDriverX
	{
		protected DeviceX Device;

		/// <summary>
		/// Sets up the this device.
		/// </summary>
		/// <param name="device">The device.</param>
		public virtual void Setup(DeviceX device)
		{
			this.Device = device;
			Device.Status = DeviceStatus.Initializing;

			Initialize();
		}

		/// <summary>
		/// Initializes this device.
		/// </summary>
		protected abstract void Initialize();

		/// <summary>
		/// Probes this instance.
		/// </summary>
		/// <remarks>Overide for ISA devices, if example</remarks>
		/// <returns></returns>
		public virtual void Probe()
		{
			Device.Status = DeviceStatus.NotFound;
		}

		/// <summary>
		/// Starts this hardware device.
		/// </summary>
		/// <returns></returns>
		public virtual void Start()
		{
			Device.Status = DeviceStatus.Error;
		}

		/// <summary>
		/// Stops this hardware device.
		/// </summary>
		/// <returns></returns>
		public virtual void Stop()
		{
		}

		/// <summary>
		/// Called when an interrupt is received.
		/// </summary>
		/// <returns></returns>
		public abstract bool OnInterrupt();
	}
}
