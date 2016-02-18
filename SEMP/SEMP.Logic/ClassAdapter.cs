using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using SEMP.Controls;
using System.Collections;
using System.Collections.ObjectModel;

namespace SEMP.Logic.WCF.Services
{//luis - 2011-10-12

    //NamedComboBoxAdapters
    public partial class RelativeGetActiveOfUserAllCompletedEventArgs : IEventArgs<ObservableCollection<NamedEntity>> { } //luis - 2012-06-19
    public partial class ProductGetActiveAllCompletedEventArgs : IEventArgs<ObservableCollection<NamedEntity>> { } //luis - 2011-11-17
    public partial class ProductGetAllCompletedEventArgs : IEventArgs<ObservableCollection<NamedEntity>> { }
    public partial class UserGetAllCompletedEventArgs : IEventArgs<ObservableCollection<NamedEntity>> { } //luis - 2011-10-24
    public partial class SolicitorKindGetAllCompletedEventArgs : IEventArgs<ObservableCollection<NamedEntity>> { } //FRL - 2011-11-04
    public partial class RejectionReasonGetAllCompletedEventArgs : IEventArgs<ObservableCollection<NamedEntity>> { } //FRL - 2011-11-11
    public partial class SolicitudeStatusGetAllCompletedEventArgs : IEventArgs<ObservableCollection<NamedEntity>> { } //FRL - 2011-11-14
    public partial class UserActiveGetAllCompletedEventArgs : IEventArgs<ObservableCollection<NamedEntity>> { } //luis - 2011-12-01
    public partial class ProductGetActiveOfUserAllCompletedEventArgs : IEventArgs<ObservableCollection<NamedEntity>> { } //luis - 2011-12-05
    public partial class ProductOriginGetAllCompletedEventArgs : IEventArgs<ObservableCollection<NamedEntityS>> { } //luis - 2012-06-13

    public partial class KinshipKindGetAllCompletedEventArgs : IEventArgs<ObservableCollection<NamedEntity>> { } //luis - 2011-12-01
    public partial class KinshipGetAllCompletedEventArgs : IEventArgs<ObservableCollection<NamedEntity>> { } //luis - 2011-12-01
    public partial class TreatmentGetAllCompletedEventArgs : IEventArgs<ObservableCollection<NamedEntity>> { } //luis - 2012-06-21

    public partial class RejectionReasonGetAllCompletedEventArgs : IEventArgs<ObservableCollection<NamedEntity>> { } //FRL - 2012-06-29

    public partial class LocationGetAllCompletedEventArgs : IEventArgs<ObservableCollection<NamedEntity>> { } //luis - 2012-07-05

    //ABMAdapters

    public abstract class RejectionReasonPageControl : ABMPageControl<RejectionReason> { }
    public partial class RejectionReasonGetCompletedEventArgs : IEventArgs<ObservableCollection<RejectionReason>> { }
    public partial class RejectionReasonSaveCompletedEventArgs : IEventArgs<Int32?> { }

    public abstract class SolicitorKindPageControl : ABMPageControl<SolicitorKind> { }
    public partial class SolicitorKindGetCompletedEventArgs : IEventArgs<ObservableCollection<SolicitorKind>> { }
    public partial class SolicitorKindSaveCompletedEventArgs : IEventArgs<Int32?> { }

    public abstract class UserPageControl : ABMPageControl<User> { }
    public partial class UserGetCompletedEventArgs : IEventArgs<ObservableCollection<User>> { }
    public partial class UserSaveCompletedEventArgs : IEventArgs<Int32?> { }

    public abstract class ProductPageControl : ABMPageControl<Product> { }
    public partial class ProductGetCompletedEventArgs : IEventArgs<ObservableCollection<Product>> { }
    public partial class ProductSaveCompletedEventArgs : IEventArgs<Int32?> { }

    public abstract class KinshipPageControl : ABMPageControl<Kinship> { }
    public partial class KinshipGetCompletedEventArgs : IEventArgs<ObservableCollection<Kinship>> { }
    public partial class KinshipSaveCompletedEventArgs : IEventArgs<Int32?> { }
    public abstract class RelativePageControl : ABMPageControl<Relative> { }
    public partial class RelativeGetCompletedEventArgs : IEventArgs<ObservableCollection<Relative>> { }
    public partial class RelativeSaveCompletedEventArgs : IEventArgs<Int32?> { }

    public abstract class LocationPageControl : ABMPageControl<Location> { }
    public partial class LocationGetCompletedEventArgs : IEventArgs<ObservableCollection<Location>> { }
    public partial class LocationSaveCompletedEventArgs : IEventArgs<Int32?> { }

}
