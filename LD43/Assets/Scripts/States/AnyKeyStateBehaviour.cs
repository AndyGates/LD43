using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyKeyStateBehaviour : StateBehaviour {

    public override string ShouldExitAt()
    {
        if(Input.anyKeyDown)
        {
            return m_exitState;
        }

        return null;
    }
}
