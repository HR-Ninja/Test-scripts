using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{

    void LoadData(GameData data);
    void SavaData(GameData data);

}
