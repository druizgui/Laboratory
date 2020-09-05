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
    public class Post
    {
        public Blog Blog { get; set; }

        public int BlogId { get; set; }
        public string Content { get; set; }
        public int PostId { get; set; }
        public string Title { get; set; }
    }
}