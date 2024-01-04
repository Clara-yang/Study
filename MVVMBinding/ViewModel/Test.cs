using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMBinding.ViewModel
{
    public class Test : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #region property
        private string m_HollowDirection;
        public string HollowDirection
        {
            get { return m_HollowDirection; }
            set
            {
                m_HollowDirection = value;
                OnPropertyChanged("HollowDirection");
            }
        }
        private string m_HollowVelocity;
        public string HollowVelocity
        {
            get { return m_HollowVelocity; }
            set
            {
                m_HollowVelocity = value;
                OnPropertyChanged("HollowVelocity");
            }
        }

        private string m_GroundWindDirection;
        public string GroundWindDirection
        {
            get { return m_GroundWindDirection; }
            set
            {
                m_GroundWindDirection = value;
                OnPropertyChanged("GroundWindDirection");
            }
        }

        private string m_GroundWindVelocity;
        public string GroundWindVelocity
        {
            get { return m_GroundWindVelocity; }
            set
            {
                m_GroundWindVelocity = value;
                OnPropertyChanged("GroundWindVelocity");
            }
        }
        private string m_GustSpeed;
        public string GustSpeed
        {
            get { return m_GustSpeed; }
            set
            {
                m_GustSpeed = value;
                OnPropertyChanged("GustSpeed");
            }
        }
        #endregion



    }
}
