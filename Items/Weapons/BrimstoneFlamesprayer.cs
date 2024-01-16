using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons
{
	public class BrimstoneFlamesprayer : ModItem
	{
		public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	    {
		    //texture =("CalamityModClassic1Point1/Items/Weapons/6.Calamitas/BrimstoneFlamesprayer");
		    return true;
	    }
	
	    public override void SetDefaults()
	    {
		    //Tooltip.SetDefault("Havoc's Breath");
			Item.damage = 59;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 50;
			Item.height = 18;
			////Tooltip.SetDefault("Consumes gel");
			Item.useTime = 9;
			Item.useAnimation = 30;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 1.5f;
			Item.UseSound = SoundID.Item34;
			Item.value = 500000;
			Item.rare = 6;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("BrimstoneFireFriendly").Type; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 8.5f;
			Item.useAmmo = 23;
		}
	}
}