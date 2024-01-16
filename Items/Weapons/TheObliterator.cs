using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class TheObliterator : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/10.DevourerofGods/TheObliterator");
		return true;
	}
	
    public override void SetDefaults()
    {
    	Item.CloneDefaults(ItemID.TheEyeOfCthulhu);
        //Tooltip.SetDefault("The Obliterator");
        Item.damage = 265;
        Item.useTime = 22;
        Item.useAnimation = 22;
        Item.useStyle = 5;
        ////Tooltip.SetDefault("Fires death lasers when enemies are near");
        Item.channel = true;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.knockBack = 6.5f;
        Item.value = 1250000;
        Item.rare = 10;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("TheObliterator").Type;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile.NewProjectile(source, position, velocity,type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
		return false;
	}
}}