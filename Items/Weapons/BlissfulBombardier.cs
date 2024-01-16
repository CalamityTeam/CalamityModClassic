using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class BlissfulBombardier : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/9.Providence/BlissfulBombardier");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Blissful Bombardier");
        Item.damage = 133;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 66;
        Item.height = 28;
        ////Tooltip.SetDefault("Fires flare rockets");
        Item.useTime = 18;
        Item.useAnimation = 18;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 6.5f;
        Item.value = 5000000;
        Item.rare = 10;
        Item.UseSound = SoundID.Item11;
        Item.autoReuse = true;
        Item.shootSpeed = 24f;
        Item.shoot = Mod.Find<ModProjectile>("Nuke").Type;
        Item.useAmmo = 771;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	Projectile.NewProjectile(source, position, velocity,Mod.Find<ModProjectile>("Nuke").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
    	return false;
	}
}}