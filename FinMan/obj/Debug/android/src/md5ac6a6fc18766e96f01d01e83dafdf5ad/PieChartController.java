package md5ac6a6fc18766e96f01d01e83dafdf5ad;


public class PieChartController
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.nulana.NChart.NChartSeriesDataSource
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_extraPoints:(Lcom/nulana/NChart/NChartSeries;)[Lcom/nulana/NChart/NChartPoint;:GetExtraPoints_Lcom_nulana_NChart_NChartSeries_Handler:NChart3D_Android.INChartSeriesDataSourceInvoker, NChart3D\n" +
			"n_image:(Lcom/nulana/NChart/NChartSeries;)Landroid/graphics/Bitmap;:GetImage_Lcom_nulana_NChart_NChartSeries_Handler:NChart3D_Android.INChartSeriesDataSourceInvoker, NChart3D\n" +
			"n_name:(Lcom/nulana/NChart/NChartSeries;)Ljava/lang/String;:GetName_Lcom_nulana_NChart_NChartSeries_Handler:NChart3D_Android.INChartSeriesDataSourceInvoker, NChart3D\n" +
			"n_points:(Lcom/nulana/NChart/NChartSeries;)[Lcom/nulana/NChart/NChartPoint;:GetPoints_Lcom_nulana_NChart_NChartSeries_Handler:NChart3D_Android.INChartSeriesDataSourceInvoker, NChart3D\n" +
			"";
		mono.android.Runtime.register ("DifferentCharts.PieChartController, FinMan, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", PieChartController.class, __md_methods);
	}


	public PieChartController () throws java.lang.Throwable
	{
		super ();
		if (getClass () == PieChartController.class)
			mono.android.TypeManager.Activate ("DifferentCharts.PieChartController, FinMan, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public PieChartController (com.nulana.NChart.NChartView p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == PieChartController.class)
			mono.android.TypeManager.Activate ("DifferentCharts.PieChartController, FinMan, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "NChart3D_Android.NChartView, NChart3D, Version=2.9.1.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}


	public com.nulana.NChart.NChartPoint[] extraPoints (com.nulana.NChart.NChartSeries p0)
	{
		return n_extraPoints (p0);
	}

	private native com.nulana.NChart.NChartPoint[] n_extraPoints (com.nulana.NChart.NChartSeries p0);


	public android.graphics.Bitmap image (com.nulana.NChart.NChartSeries p0)
	{
		return n_image (p0);
	}

	private native android.graphics.Bitmap n_image (com.nulana.NChart.NChartSeries p0);


	public java.lang.String name (com.nulana.NChart.NChartSeries p0)
	{
		return n_name (p0);
	}

	private native java.lang.String n_name (com.nulana.NChart.NChartSeries p0);


	public com.nulana.NChart.NChartPoint[] points (com.nulana.NChart.NChartSeries p0)
	{
		return n_points (p0);
	}

	private native com.nulana.NChart.NChartPoint[] n_points (com.nulana.NChart.NChartSeries p0);

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
