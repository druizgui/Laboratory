// -----------------------------------------------------------------
// <copyright>Copyright (C) 2020, David Ruiz.</copyright>
// Licensed under the Apache License, Version 2.0.
// You may not use this file except in compliance with the License:
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Software is distributed on an "AS IS", WITHOUT WARRANTIES
// OR CONDITIONS OF ANY KIND, either express or implied.
// -----------------------------------------------------------------

namespace Demo.Data.Multiprovider
{
    using System.Collections.Generic;

    public class Blog
    {
        public int BlogId { get; set; }

        public List<Post> Posts { get; } = new List<Post>();
        public string Url { get; set; }
    }
}