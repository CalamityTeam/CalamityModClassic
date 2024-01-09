using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.Plaguebringer {
public class PestilentDefiler : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Pestilent Defiler");
	}

    public override void SetDefaults()
    {
        Item.damage = 135;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 46;
        Item.height = 20;
        Item.useTime = 37;
        Item.useAnimation = 37;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 9.5f;
        Item.value = 950000;
        Item.rare = ItemRarityID.Cyan;
        Item.UseSound = SoundID.Item40;
        Item.autoReuse = false;
        Item.shootSpeed = 20f;
        Item.shoot = Mod.Find<ModProjectile>("SicknessRound").Type;
        Item.useAmmo = 97;
    }
    
    public override Vector2? HoldoutOffset()
	{
		return new Vector2(0, -5);
	}
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("SicknessRound").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
    	return false;
	}
}}