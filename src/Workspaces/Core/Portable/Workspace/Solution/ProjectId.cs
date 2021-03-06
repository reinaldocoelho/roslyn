﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.CodeAnalysis
{
    /// <summary>
    /// An identifier that can be used to refer to the same <see cref="Project"/> across versions.
    /// </summary>
    [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
    public sealed class ProjectId : IEquatable<ProjectId>
    {
        private readonly string _debugName;

        /// <summary>
        /// The system generated unique id.
        /// </summary>
        public Guid Id { get; }

        private ProjectId(string debugName)
        {
            this.Id = Guid.NewGuid();
            _debugName = debugName;
        }

        internal ProjectId(Guid guid, string debugName)
        {
            this.Id = guid;
            _debugName = debugName;
        }

        /// <summary>
        /// Create a new ProjectId instance.
        /// </summary>
        /// <param name="debugName">An optional name to make this id easier to recognize while debugging.</param>
        public static ProjectId CreateNewId(string debugName = null)
        {
            return new ProjectId(debugName);
        }

        public static ProjectId CreateFromSerialized(Guid id, string debugName = null)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException(nameof(id));
            }

            return new ProjectId(id, debugName);
        }

        private string GetDebuggerDisplay()
        {
            return string.Format("({0}, #{1} - {2})", this.GetType().Name, this.Id, _debugName);
        }

        internal string DebugName
        {
            get { return _debugName; }
        }

        public override string ToString()
        {
            return GetDebuggerDisplay();
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ProjectId);
        }

        public bool Equals(ProjectId other)
        {
            return
                !ReferenceEquals(other, null) &&
                this.Id == other.Id;
        }

        public static bool operator ==(ProjectId left, ProjectId right)
        {
            return EqualityComparer<ProjectId>.Default.Equals(left, right);
        }

        public static bool operator !=(ProjectId left, ProjectId right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
