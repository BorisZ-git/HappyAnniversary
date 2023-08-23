using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinksHash
{
    public static LinksHash Links { get; private set; }

    public LinksHash()
    {
        Links = this;
    }
}
