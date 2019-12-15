using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Common.Graphics
{
    public class SphericalCoordinateCamera : Camera
    {
        private double theta;
        private double phi;
        private double distance;

        public double Theta
        {
            get
            {
                return theta;
            }
            set
            {
                theta = value;
                UpdateParameters();
            }
        }

        public double Phi
        {
            get
            {
                return phi;
            }
            set
            {
                phi = value;
                UpdateParameters();
            }
        }

        public double Distance
        {
            get
            {
                return distance;
            }
            set
            {
                distance = value;
                UpdateParameters();
            }
        }

        public SphericalCoordinateCamera(double theta, double phi, double distance)
            : base(new Vector3(0, 0, 0), new Vector3(1, 0, 0), Vector3.UnitY)
        {
            this.theta = theta;
            this.phi = phi;
            this.distance = distance;

            UpdateParameters();
        }

        public void UpdateParameters()
        {
            double x = Math.Sin(theta) * Math.Sin(phi) * distance;
            double y = Math.Cos(phi) * distance;
            double z = Math.Cos(theta) * Math.Sin(phi) * distance;

            Position = Target + new Vector3(x, y, z);
        }
    }
}
