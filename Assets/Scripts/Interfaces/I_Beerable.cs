using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_Beerable {
	void GetBeered(Collision beerCollider);
	bool HasBeenBeered { get; set; }
}
