using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICircuitComponent
{
	List<ICircuitComponent> SeekNext();
	List<ICircuitComponent> SeekPrev();
	List<ICircuitComponent> Peek();
	void Execute();
}
