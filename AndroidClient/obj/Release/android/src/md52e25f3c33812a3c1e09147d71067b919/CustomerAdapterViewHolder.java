package md52e25f3c33812a3c1e09147d71067b919;


public class CustomerAdapterViewHolder
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("AndroidClient.Adapters.CustomerAdapterViewHolder, AndroidClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", CustomerAdapterViewHolder.class, __md_methods);
	}


	public CustomerAdapterViewHolder ()
	{
		super ();
		if (getClass () == CustomerAdapterViewHolder.class)
			mono.android.TypeManager.Activate ("AndroidClient.Adapters.CustomerAdapterViewHolder, AndroidClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
