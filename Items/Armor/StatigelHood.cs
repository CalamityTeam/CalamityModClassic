using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items.Armor;

namespace CalamityModClassicPreTrailer.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class StatigelHood : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Statigel Hood");
            // Tooltip.SetDefault("Increased minion knockback and +1 max minion");
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
			Item.value = Item.buyPrice(0, 5, 0, 0);
			Item.rare = 4;
            Item.defense = 4; //20
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("StatigelArmor").Type && legs.type == Mod.Find<ModItem>("StatigelGreaves").Type;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "18% increased minion damage\n" +
                "Summons a mini slime god to fight for you, the type depends on what world evil you have\n" +
                "When you take over 100 damage in one hit you become immune to damage for an extended period of time\n" +
                "Grants an extra jump and increased jump height";
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.statigelSet = true;
            modPlayer.slimeGod = true;
            player.GetJumpState(ExtraJump.TsunamiInABottle).Enable();
            player.jumpBoost = true;
            player.GetDamage(DamageClass.Summon) += 0.18f;
            if (player.whoAmI == Main.myPlayer)
            {
                if (player.FindBuffIndex(Mod.Find<ModBuff>("SlimeGod").Type) == -1)
                {
                    player.AddBuff(Mod.Find<ModBuff>("SlimeGod").Type, 3600, true);
                }
                if (WorldGen.crimson && player.ownedProjectileCounts[Mod.Find<ModProjectile>("SlimeGodAlt").Type] < 1)
                {
                    Projectile.NewProjectile(Entity.GetSource_FromThis(null), player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("SlimeGodAlt").Type, (int)(33f * player.GetDamage(DamageClass.Summon).Multiplicative), 0f, Main.myPlayer, 0f, 0f);
                }
                else if (!WorldGen.crimson && player.ownedProjectileCounts[Mod.Find<ModProjectile>("SlimeGod").Type] < 1)
                {
                    Projectile.NewProjectile(Entity.GetSource_FromThis(null), player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("SlimeGod").Type, (int)(33f * player.GetDamage(DamageClass.Summon).Multiplicative), 0f, Main.myPlayer, 0f, 0f);
                }
            }
        }

        public override void UpdateEquip(Player player)
        {
            player.GetKnockback(DamageClass.Summon).Base += 1.5f;
            player.maxMinions++;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "PurifiedGel", 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 9);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}