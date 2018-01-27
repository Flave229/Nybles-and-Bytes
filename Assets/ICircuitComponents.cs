using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICircuitComponents
{
	List<ICircuitComponents> SeekNext();
	List<ICircuitComponents> SeekPrev();
	List<ICircuitComponents> Peek();
	void Execute();
}
