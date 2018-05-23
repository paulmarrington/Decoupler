using System;
using UnityEngine;

namespace Decoupled {
  /// <inheritdoc />
  /// <summary>
  /// Superclassf or a decoupled component Monobehaviour
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public abstract class ComponentDecoupler<T> : MonoBehaviour where T : ComponentDecoupler<T> {
    /// <summary>
    /// Actions set by alternative components to see who is boss
    /// </summary>
    protected static event Action<T> Initialisers = delegate { };

    /// <summary>
    /// Object reference to interface data
    /// </summary>
    // ReSharper disable once StaticMemberInGenericType
    protected static object InterfaceData;

    /// <summary>
    /// Retrieve the component type of the default component
    /// </summary>
    protected abstract Type DefaultComponent { get; }

    /// <summary>
    /// Called when component is loaded or reset from the Inspector menu
    /// </summary>
    protected void Reset() {
      Initialisers(this as T);

      Debug.LogWarningFormat(
        "**** ComponentDecoupler:32 InterfaceData={0}  #### DELETE-ME #### 23/5/18 4:58 PM",
        InterfaceData); //#DM#//

      if ((InterfaceData != null) || (DefaultComponent == null)) return;

      gameObject.AddComponent(DefaultComponent);
      Initialisers(this as T);
    }
  }
}