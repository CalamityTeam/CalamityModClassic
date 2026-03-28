using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
//using TerrariaOverhaul;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class MandibleBow : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mandible Bow");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 13;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 22;
	        Item.height = 40;
	        Item.useTime = 25;
	        Item.useAnimation = 25;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 2f;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = 1;
	        Item.UseSound = SoundID.Item5;
	        Item.autoReuse = false;
	        Item.shoot = 10;
	        Item.shootSpeed = 30f;
	        Item.useAmmo = 40;
	    }

        /*public void OverhaulInit()
        {
            this.SetTag("bow");
        }*/
    }
}