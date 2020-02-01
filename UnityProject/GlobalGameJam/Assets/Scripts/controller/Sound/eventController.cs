using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventController : MonoBehaviour
{
	public static eventController Instance { get; private set; }

	public AK.Wwise.Event AMB_jour_play;
	public AK.Wwise.Event AMB_stop;
	public AK.Wwise.Event AMB_switch_jour;
	public AK.Wwise.Event AMB_switch_nuit;

	public AK.Wwise.Event FS_eau;
	public AK.Wwise.Event FS_sable_course;
	public AK.Wwise.Event FS_sable_marche;

	public AK.Wwise.Event ACT_algue_use;
	public AK.Wwise.Event ACT_bois_use;
	public AK.Wwise.Event ACT_sable_use;
	public AK.Wwise.Event ACT_superBois_use;

	public AK.Wwise.Event ACT_algue_pickUp;
	public AK.Wwise.Event ACT_bois_pickUp;
	public AK.Wwise.Event ACT_eau_pickUp;
	public AK.Wwise.Event ACT_sable_pickUp;
	public AK.Wwise.Event ACT_superBois_pickUp;

	private void Awake()
	{
		Instance = this;
		DontDestroyOnLoad(Instance);
	}
}
